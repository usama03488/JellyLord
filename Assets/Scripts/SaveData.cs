using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public bool tutorialDone;
    public Color blockColor;
    public Levels levels;
    public List<Stars> stars;

    public SaveData()
    {
        levels = new Levels();
        stars = new List<Stars>();
        tutorialDone = false;
        blockColor = new Color(0.8301887f, 0f, 0f, 1f);
    }
    
    public SaveData(Chapters chp, int lvl, Color bColor)
    {
        levels = new Levels(chp, lvl);
        blockColor = bColor;
        tutorialDone = true;
    }
    
    
}

[Serializable]
public class Levels
{
    public Chapters chapter;
    public int completedLevel;
    
    public Levels()
    {
        chapter = Chapters.Chapter1;
        completedLevel = 0;
    }
    
    public Levels(Chapters chp, int lvl)
    {
        chapter = chp;
        completedLevel = lvl;
    }
}

[Serializable]
public class Stars
{
    public Chapters chapter;
    public int level;
    public int star;
    
    public Stars()
    {
        chapter = Chapters.Chapter1;
        level = 0;
    }
    
    public Stars(Chapters chp, int lvl, int s)
    {
        chapter = chp;
        level = lvl;
        star = s;
    }
}

[Serializable]
public enum Chapters
{
    Chapter1,
    Chapter2,
    Chapter3
}