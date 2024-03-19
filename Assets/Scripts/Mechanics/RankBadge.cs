using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankBadge : MonoBehaviour
{
    public TextMeshProUGUI rankText;

    public void SetRank(int rank)
    {
        rankText.text = rank.ToString();
    }
}
