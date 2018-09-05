﻿using ArabicSupport;
using MgsCommonLib.Theme;
using UnityEngine;
using UnityEngine.UI;

class UIUserPuzzleMenuItem : UIMenuItem
{

    public Text ClueText;
    public Image RateImage;
    public Text PlayCount;
    public Text State;

    protected override void Refresh(object data)
    {
        var puzzle = (UserPuzzle)data;

        ClueText.text = ArabicFixer.Fix(puzzle.Clue);

        RateImage.gameObject.SetActive(puzzle.Rate != null);
        PlayCount.gameObject.SetActive(puzzle.PlayCount.HasValue && puzzle.PlayCount > 0);

        //State.gameObject.SetActive(!string.IsNullOrEmpty(puzzle.CategoryName));
        State.gameObject.SetActive(true);

        State.text =
            puzzle.ServerID == null ? ThemeManager.Instance.LanguagePack.GetLable("NotRegister") :
            puzzle.CategoryName == null ? ThemeManager.Instance.LanguagePack.GetLable("InReview") :
            puzzle.CategoryName == "" ? ThemeManager.Instance.LanguagePack.GetLable("NoCategory") :
            ArabicFixer.Fix(puzzle.CategoryName);


        if (puzzle.PlayCount != null) PlayCount.text = puzzle.PlayCount.ToString();

        if (puzzle.Rate != null) RateImage.fillAmount = puzzle.Rate.Value / 5f;

        GetComponent<RectTransform>().localScale = Vector3.one;
    }


}