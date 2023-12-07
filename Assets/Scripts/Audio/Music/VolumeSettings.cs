using System;
using UnityEngine;
using UnityEngine.Timeline;

public class VolumeSettings
{
    public int Master = 100;
    public int Music = 100;
    public int Sfx = 100;

    public byte[] ToByteArray()
    {
        int[] values = { Master, Music, Sfx };
        byte[] result = new byte[values.Length * sizeof(int)];

        for (int i = 0; i < values.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(values[i]);
            bytes.CopyTo(result, i * sizeof(int));
        }
        return result;
    }

    // Deserialize a byte array to VolumeSettings
    public static VolumeSettings FromByteArray(byte[] bytes)
    {
        VolumeSettings settings = new VolumeSettings();

        settings.Master = BitConverter.ToInt32(bytes, 0 * sizeof(int));
        settings.Music = BitConverter.ToInt32(bytes, 1 * sizeof(int));
        settings.Sfx = BitConverter.ToInt32(bytes, 2 * sizeof(int));

        return settings;
    }
}