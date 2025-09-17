using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class QuizItem
{
    public string question;
    public List<int> answers;
    public int correct; // the correct *value* (compare selected value == correct)
}

[Serializable]
public class QuizSet
{
    public List<QuizItem> items;
}
