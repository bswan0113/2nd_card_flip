using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OpenStage()
    {
        //��������1,2 Ŭ����� �۵�
        if (stagePn != null)
        {
            stagePn.SetActive(true);
        }
        else
        {
            Debug.LogWarning("�Ʒ����������� Ŭ������� �ʾҽ��ϴ�");
        }
    }
    public void Ending()
    {
        //3�������� Ŭ����� ����ȭ��
        if (end != null)
        {
            end.SetActive(true);
        }
        else
        {
            Debug.LogWarning("�Ʒ����������� Ŭ������� �ʾҽ��ϴ�");
        }
    }
}

