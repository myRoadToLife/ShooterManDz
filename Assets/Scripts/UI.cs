using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public readonly string WinMessageTimeOver = "����� �����! ����� �������!";
    public readonly string WinMessageEnemiesDefeated = "����� ���� ����� ������ � �������!";
    public readonly string LoseMessagePlayerDied = "����� ���� �� ������ �����!";
    public readonly string LoseMessageArenaCaptured = "����� ��������� �����, ����� ��������!";

    public readonly string ObjectiveKillEnemies = "������ ����� �����: ";
    public readonly string ObjectivePlayerHealth = "� ������ �������� ��������: ";
    public readonly string ObjectiveCaptureArena = "��� ������� ����� �����: ";
    public readonly string ObjectiveHoldArena = "���������� �� �����: ";

    public readonly string TextEnemy = " ������!";
    public readonly string TextTime = " ������!";


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
