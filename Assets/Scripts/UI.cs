using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public readonly string WinMessageTimeOver = "Время вышло! Игрок победил!";
    public readonly string WinMessageEnemiesDefeated = "Игрок убил много врагов и победил!";
    public readonly string LoseMessagePlayerDied = "Игрок умер от потери крови!";
    public readonly string LoseMessageArenaCaptured = "Враги захватили арену, Игрок проиграл!";

    public readonly string ObjectiveKillEnemies = "Игроку нужно убить: ";
    public readonly string ObjectivePlayerHealth = "У игрока осталось здоровья: ";
    public readonly string ObjectiveCaptureArena = "Для захвата арены нужно: ";
    public readonly string ObjectiveHoldArena = "Продержись на арене: ";

    public readonly string TextEnemy = " врагов!";
    public readonly string TextTime = " секунд!";


    [field: SerializeField] public TMP_Text EventText { get; private set; }
    [field: SerializeField] public TMP_Text WinCounterText { get; private set; }
    [field: SerializeField] public TMP_Text WinObjectiveText { get; private set; }
    [field: SerializeField] public TMP_Text LoseCounterText { get; private set; }
    [field: SerializeField] public TMP_Text LoseObjectiveTex { get; private set; }

    public void TextMessageEvent(Color color, string textEvent)
    {
        EventText.gameObject.SetActive(true);
        EventText.color = color;
        EventText.text = textEvent;
    }

    public void TextCounter(TMP_Text textCounter, TMP_Text textNumberUnits, float counter, float numberUnits, string firstText, string secondText)
    {
        textCounter.gameObject.SetActive(true);
        textNumberUnits.gameObject.SetActive(true);

        textCounter.text = counter.ToString("0");
        textNumberUnits.text = firstText + numberUnits.ToString("0") + secondText;
    }

    public void DisableTextObjects()
    {
        WinCounterText.gameObject.SetActive(false);
        LoseCounterText.gameObject.SetActive(false);

        WinObjectiveText.gameObject.SetActive(false);
        LoseObjectiveTex.gameObject.SetActive(false);
    }
}
