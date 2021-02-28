using System.Threading.Tasks;
using Kadoy.CrimeNet.Models.Missions;
using Kadoy.CrimeNet.Utils;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Kadoy.CrimeNet.Missions {
  public class MissionInformationBehaviour : MonoBehaviour {
    private const float InfoAppearanceDuration = 0.3f;
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

    private Sequence sequence;

    public void Initialize(InnerMissionInfo missionInfo) {
      titleText.text = missionInfo.Name.ToUpper();
      authorText.text = string.Format(AuthorFormat, missionInfo.Author.ToUpper());
      conditionsText.text = missionInfo.Conditions.ToUpper();
      difficultyText.text = missionInfo.DifficultyDescription.ToUpper();
      experienceText.text = missionInfo.Experience.ToUpper();

      difficultyMarksParent.ActivateChildren(missionInfo.Difficulty);
    }

    public void ShowDetailedInfo() {
      sequence?.Kill();
      sequence = DOTween.Sequence();
      
      GameObjects.Active(authorText, conditionsParent, difficultyTextParent, experienceParent);
      
      sequence.Join(titleParent.DOLocalMoveX(authorText.bounds.size.x,  InfoAppearanceDuration));
      sequence.Join(conditionsParent.DOLocalMoveX(conditionsText.bounds.size.x, InfoAppearanceDuration));
      sequence.Join(difficultyTextParent.DOLocalMoveX(difficultyText.bounds.size.x, InfoAppearanceDuration));
      sequence.Join(experienceParent.DOLocalMoveX(experienceText.bounds.size.x, InfoAppearanceDuration));
      sequence.Join(difficultyMarksParent.DOLocalMoveY(-100, InfoAppearanceDuration));
    }

    public void HideDetailedInfo() {
      sequence?.Kill();
      sequence = DOTween.Sequence();

      sequence.Join(titleParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      sequence.Join(conditionsParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      sequence.Join(difficultyTextParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      sequence.Join(experienceParent.transform.DOLocalMoveX(0, InfoAppearanceDuration));
      sequence.Join(difficultyMarksParent.transform.DOLocalMoveY(0, InfoAppearanceDuration));

      sequence.OnComplete(DeactivateInfoText);

      void DeactivateInfoText() {
        GameObjects.NotActive(authorText, conditionsParent, difficultyTextParent, experienceParent);
      }
    }
  }
}