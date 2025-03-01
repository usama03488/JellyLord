﻿#if POSEIDON_URP
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pinwheel.Poseidon.FX.Universal
{
    public class PWaterEffectRendererFeature : ScriptableRendererFeature
    {
        public class PWaterEffectPass : ScriptableRenderPass
        {
            public const string PROFILER_TAG = "Water FX";

            private Material underwaterMaterial;
            private Material wetLensMaterial;

#if UNITY_2022_1_OR_NEWER
#pragma warning disable 0649
            private RTHandle cameraTarget = null;
            private RTHandle temporaryRenderTexture = null;
#pragma warning restore 0649
#else
            private RenderTargetIdentifier cameraTarget;
            private RenderTargetHandle temporaryRenderTexture;
#endif

            private static Shader underwaterShader;
            private static Shader UnderwaterShader
            {
                get
                {
                    if (underwaterShader == null)
                    {
                        underwaterShader = Shader.Find(PWaterFX.UNDERWATER_SHADER_URP);
                    }
                    return underwaterShader;
                }
            }

            private static Shader wetLensShader;
            private static Shader WetLensShader
            {
                get
                {
                    if (wetLensShader == null)
                    {
                        wetLensShader = Shader.Find(PWaterFX.WETLENS_SHADER_URP);
                    }
                    return wetLensShader;
                }
            }

            public PWaterEffectPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
            }

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {

            }

            private void ConfigureMaterial(ref RenderingData renderingData, PUnderwaterOverride underwater, PWetLensOverride wetLens)
            {
                if (underwater.intensity.value > 0)
                {
                    if (underwaterMaterial == null)
                    {
                        Shader shader = UnderwaterShader;
                        underwaterMaterial = CoreUtils.CreateEngineMaterial(shader);
                    }

                    underwaterMaterial.SetFloat(PMat.PP_WATER_LEVEL, underwater.waterLevel.value);
                    underwaterMaterial.SetFloat(PMat.PP_MAX_DEPTH, underwater.maxDepth.value);
                    underwaterMaterial.SetFloat(PMat.PP_SURFACE_COLOR_BOOST, underwater.surfaceColorBoost.value);

                    underwaterMaterial.SetColor(PMat.PP_SHALLOW_FOG_COLOR, underwater.shallowFogColor.value);
                    underwaterMaterial.SetColor(PMat.PP_DEEP_FOG_COLOR, underwater.deepFogColor.value);
                    underwaterMaterial.SetFloat(PMat.PP_VIEW_DISTANCE, underwater.viewDistance.value);

                    if (underwater.enableCaustic.value == true)
                    {
                        underwaterMaterial.EnableKeyword(PMat.KW_PP_CAUSTIC);
                        underwaterMaterial.SetTexture(PMat.PP_CAUSTIC_TEX, underwater.causticTexture.value);
                        underwaterMaterial.SetFloat(PMat.PP_CAUSTIC_SIZE, underwater.causticSize.value);
                        underwaterMaterial.SetFloat(PMat.PP_CAUSTIC_STRENGTH, underwater.causticStrength.value);
                    }
                    else
                    {
                        underwaterMaterial.DisableKeyword(PMat.KW_PP_CAUSTIC);
                    }

                    if (underwater.enableDistortion.value == true)
                    {
                        underwaterMaterial.EnableKeyword(PMat.KW_PP_DISTORTION);
                        underwaterMaterial.SetTexture(PMat.PP_DISTORTION_TEX, underwater.distortionNormalMap.value);
                        underwaterMaterial.SetFloat(PMat.PP_DISTORTION_STRENGTH, underwater.distortionStrength.value);
                        underwaterMaterial.SetFloat(PMat.PP_WATER_FLOW_SPEED, underwater.waterFlowSpeed.value);
                    }
                    else
                    {
                        underwaterMaterial.DisableKeyword(PMat.KW_PP_DISTORTION);
                    }

                    underwaterMaterial.SetTexture(PMat.PP_NOISE_TEX, PPoseidonSettings.Instance.NoiseTexture);
                    underwaterMaterial.SetVector(PMat.PP_CAMERA_VIEW_DIR, renderingData.cameraData.camera.transform.forward);
                    underwaterMaterial.SetFloat(PMat.PP_CAMERA_FOV, renderingData.cameraData.camera.fieldOfView);
                    underwaterMaterial.SetMatrix(PMat.PP_CAMERA_TO_WORLD_MATRIX, renderingData.cameraData.camera.cameraToWorldMatrix);
                    underwaterMaterial.SetFloat(PMat.PP_INTENSITY, underwater.intensity.value);
                }

                if (wetLens.strength.value * wetLens.intensity.value > 0)
                {
                    if (wetLensMaterial == null)
                    {
                        Shader shader = WetLensShader;
                        wetLensMaterial = CoreUtils.CreateEngineMaterial(shader);
                    }

                    Texture normalMap = wetLens.normalMap.value ?? PPoseidonSettings.Instance.DefaultNormalMap;
                    wetLensMaterial.SetTexture(PMat.PP_WET_LENS_TEX, normalMap);
                    wetLensMaterial.SetFloat(PMat.PP_WET_LENS_STRENGTH, wetLens.strength.value * wetLens.intensity.value);
                }
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                if (renderingData.cameraData.camera != Camera.main)
                    return;

                VolumeStack stack = VolumeManager.instance.stack;
                PUnderwaterOverride underwater = stack.GetComponent<PUnderwaterOverride>();
                PWetLensOverride wetLens = stack.GetComponent<PWetLensOverride>();

                bool willRenderUnderwater = underwater.intensity.value > 0;
                bool willRenderWetLens = wetLens.strength.value * wetLens.intensity.value > 0;
                if (!willRenderUnderwater && !willRenderWetLens)
                    return;

                ConfigureMaterial(ref renderingData, underwater, wetLens);

                Material material = willRenderUnderwater ? underwaterMaterial : wetLensMaterial;
                CommandBuffer cmd = CommandBufferPool.Get(PROFILER_TAG);
                RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
                cameraTargetDescriptor.depthBufferBits = 0;

#if UNITY_2022_1_OR_NEWER
                cameraTarget = renderingData.cameraData.renderer.cameraColorTargetHandle;
                temporaryRenderTexture = RTHandles.Alloc(cameraTargetDescriptor);
                Blit(cmd, cameraTarget, temporaryRenderTexture, material, 0);
                Blit(cmd, temporaryRenderTexture, cameraTarget);
#elif UNITY_2021_2_OR_NEWER
                cameraTarget = renderingData.cameraData.renderer.cameraColorTarget;
                cmd.GetTemporaryRT(temporaryRenderTexture.id, cameraTargetDescriptor);
                Blit(cmd, cameraTarget, temporaryRenderTexture.Identifier(), material, 0);
                Blit(cmd, temporaryRenderTexture.Identifier(), cameraTarget);
#else
                cameraTarget = UniversalRenderPipeline.asset.scriptableRenderer.cameraColorTarget;
                cmd.GetTemporaryRT(temporaryRenderTexture.id, cameraTargetDescriptor);
                Blit(cmd, cameraTarget, temporaryRenderTexture.Identifier(), material, 0);
                Blit(cmd, temporaryRenderTexture.Identifier(), cameraTarget);
#endif

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            public override void FrameCleanup(CommandBuffer cmd)
            {
                //cmd.ReleaseTemporaryRT(temporaryRenderTexture.id);
            }
        }

        private PWaterEffectPass waterEffectPass;

        public override void Create()
        {
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            waterEffectPass = new PWaterEffectPass();
            renderer.EnqueuePass(waterEffectPass);
        }

    }
}
#endif