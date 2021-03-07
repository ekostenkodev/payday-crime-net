using Kadoy.CrimeNet.Models.Missions;
using Kadoy.CrimeNet.Utils;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Kadoy.CrimeNet.Util;

namespace Kadoy.CrimeNet.Missions.Bubble {
  public class MissionBubbleInformationBehaviour : MonoBehaviour {
    private const float InfoAppearanceDuration = 0.5f;
    private const float OpenDelay = 0.5f;
    private const string AuthorFormat = "{0}: ";

    [Header("TITLE")]
    [SerializeField]
    private RectTransform titleParent;

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text authorText;

    [Header("CONDITIONS")]
    [SerializeField]
    private RectTransform conditionsParent;

    [SerializeField]
    private TMP_Text conditionsText;

    [Header("DIFFICULTY")]
    [SerializeField]
    private RectTransform difficultyTextParent;

    [SerializeField]
    private TMP_Text difficultyText;

    [SerializeField]
    private RectTransform difficultyMarksParent;

    [Header("EXPERIENCE")]
    [SerializeField]
    private RectTransform experienceParent;

    [SerializeField]
    private TMP_Text experienceText;

    private Sequence moveSequence;

    public void Initialize(InnerMissionInfo missionInfo) {
      titleText.text = missionInfo.Name.ToUpper();
      authorText.text = string.Format(AuthorFormat, missionInfo.Author.ToUpper());
      conditionsText.text = missionInfo.Conditions.ToUpper();
      difficultyText.text = missionInfo.DifficultyDescription.ToUpper();
      experienceText.text = missionInfo.Experience.ToUpper();

      difficultyMarksParent.ActivateChildren(missionInfo.Difficulty);

      Open();
    }

    public void ShowDetailedInfo() {
      moveSequence?.Kill();
      moveSequence = DOTween.Sequence();

      GameObjects.Active(authorText, conditionsParent, difficultyTextParent, experienceParent);

      moveSequence.Join(titleParent.DOLocalMoveX(authorText.bounds.size.x, InfoAppearanceDuration));
      moveSequence.Join(conditionsParent.DOLocalMoveX(conditionsText.bounds.size.x, InfoAppearanceDuration));
      moveSequence.Join(difficultyTextParent.DOLocalMoveX(difficultyText.bounds.size.x, InfoAppearanceDuration));
      moveSequence.Join(experienceParent.DOLocalMoveX(experienceText.bounds.size.x, InfoAppearanceDuration));
      moveSequence.Join(difficultyMarksParent.DOLocalMoveY(-100, InfoAppearanceDuration));
    }

    public void HideDetailedInfo() {
      moveSequence?.Kill();
      moveSequence = DOTween.Sequence();

      moveSequence.Join(titleParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      moveSequence.Join(conditionsParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      moveSequence.Join(difficultyTextParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      moveSequence.Join(experienceParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      moveSequence.Join(difficultyMarksParent.transform.DOLocalMoveY(0, InfoAppearanceDuration));

      moveSequence.OnComplete(DeactivateInfoText);

      void DeactivateInfoText() {
        GameObjects.NotActive(authorText, conditionsParent, difficultyTextParent, experienceParent);
      }
    }

    public Sequence Close() {
      moveSequence?.Kill();
      moveSequence = DOTween.Sequence();

      moveSequence.Join(titleParent.transform.DOLocalMoveX(-titleText.bounds.size.x, InfoAppearanceDuration));
      moveSequence.Join(conditionsParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      moveSequence.Join(difficultyTextParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      moveSequence.Join(experienceParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      moveSequence.Join(difficultyMarksParent.DOLocalMoveY(0, InfoAppearanceDuration));
      moveSequence.Join(difficultyMarksParent.DOLocalMoveX(-difficultyMarksParent.rect.width, InfoAppearanceDuration));

      return moveSequence;
    }

    private void Open() {
      moveSequence?.Kill();
      moveSequence = DOTween.Sequence();

      titleParent.localPosition = titleParent.localPosition.WithX(-200);
      difficultyMarksParent.localPosition = difficultyMarksParent.localPosition.WithX(-200);

      moveSequence.Join(titleParent
        .DOLocalMoveX(0, InfoAppearanceDuration * 2)
        .SetDelay(OpenDelay));
      difficultyMarksParent
        .DOLocalMoveX(0, InfoAppearanceDuration * 2)
        .SetDelay(OpenDelay);
    }
  }
}