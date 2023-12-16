using System;
using System.Globalization;
using UnityEngine;

[Serializable]
public class SerializableDateTime
{
    [SerializeField] private string stringDate;

    public DateTime DateTime
    {
        get
        {
            if (DateTime.TryParse(stringDate, out var dateTime)) return dateTime;
            
            Debug.LogError($"Could not parse date time from \"{stringDate}\"");
            return default;
        }
    }

    public SerializableDateTime(DateTime dateTime)
    {
        stringDate = dateTime.ToString(CultureInfo.InvariantCulture);
    }
}