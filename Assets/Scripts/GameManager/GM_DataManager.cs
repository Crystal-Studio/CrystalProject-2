using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Security.Cryptography;
using System;

public class GM_DataManager : MonoBehaviour
{
    public TextAsset file;
    GM_Characters characters;
    Aes myAes;
    string data = null;

    void Start ()
    {
        myAes = Aes.Create();
        
        if (PlayerPrefs.GetInt("NewGame") == 0)
        {
            InitFile();
        }
        else
        {
            myAes.Key = File.ReadAllBytes(Application.persistentDataPath + "/AesKey");
            myAes.IV = File.ReadAllBytes(Application.persistentDataPath + "/AesIV");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SaveData()
    {
        byte[] encrypted;
        
        characters.characters[1].characterName = "J'ai Sauvé OK ?";
        encrypted = File.ReadAllBytes(Application.persistentDataPath + "/characters");

        string original = JsonUtility.ToJson(characters);
        
        encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

        File.WriteAllBytes(Application.persistentDataPath + "/characters", encrypted);
        
    }

    public void ResetData()
    {
        File.Delete(Application.persistentDataPath + "/characters");
        PlayerPrefs.SetInt("NewGame", 0);
    }

    public void LoadData()
    {
        FileStream fichier;
        Debug.Log(myAes.Key[0]);
        byte[] encrypted = File.ReadAllBytes(Application.persistentDataPath + "/characters");
        data = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
        
        characters = JsonUtility.FromJson<GM_Characters>(data);

        for (int i = 0; i < characters.characters.Count; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Text>().text = characters.characters[i].id.ToString();
            transform.GetChild(i).GetChild(1).GetComponent<Text>().text = characters.characters[i].characterName;
            transform.GetChild(i).GetChild(2).GetComponent<Text>().text = characters.characters[i].health.ToString();
            transform.GetChild(i).GetChild(3).GetComponent<Text>().text = characters.characters[i].power.ToString();
            transform.GetChild(i).GetChild(4).GetComponent<Text>().text = characters.characters[i].armor.ToString();
            transform.GetChild(i).GetChild(5).GetComponent<Text>().text = characters.characters[i].action.ToString();
            transform.GetChild(i).GetChild(6).GetComponent<Text>().text = characters.characters[i].movement.ToString();
            transform.GetChild(i).GetChild(7).GetComponent<Text>().text = characters.characters[i].initiative.ToString();
            transform.GetChild(i).GetChild(8).GetComponent<Text>().text = characters.characters[i].precision.ToString();
        }
    }

    private void InitFile()
    {
        PlayerPrefs.SetInt("NewGame", 1);

        data = file.ToString();
        byte[] encrypted;
        encrypted = EncryptStringToBytes_Aes(data, myAes.Key, myAes.IV);

        File.WriteAllBytes(Application.persistentDataPath + "/characters", encrypted);
        File.WriteAllBytes(Application.persistentDataPath + "/AesKey", myAes.Key);
        File.WriteAllBytes(Application.persistentDataPath + "/AesIV", myAes.IV);
    }

    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        return encrypted;

    }

    static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        }

        return plaintext;

    }
}
