﻿using System.Collections;
using MgsCommonLib.UI;
using UnityEngine.UI;

public class PhoneNumberWindow : UIWindowBase
{
    public InputField PhoneNumberInputField;
    
    public IEnumerator SendCodeButton()
    {
        // Hide phone number window
        yield return Hide();

        // Show in-progress window
        yield return UIController.ShowInprogressWindow(LanguagePack.GetLable("Inprogress_AccountRecovery"));

        // Send Random code to phone number
        yield return AccountManager
            .Instance
            .SendRandomCodeToPhoneNumber(PhoneNumberInputField.text);

        // Hide in-progress window
        yield return UIController.HideInprogressWindow();

        // Switch Send code result
        switch (AccountManager.Instance.SendCodeResult)
        {
            // شماره ثبت نشده است
            case AccountManager.SendCodeResultEnum.NotRegister:
                yield return UIController
                    .DisplayError(
                        LanguagePack.GetLable("Error_UnknownPhoneNumber"),
                        IconPack.GetIcon("UnkownPhone"));
                break;

            // شماره تلفن معتبر نیست
            case AccountManager.SendCodeResultEnum.InvalidPhoneNumber:
                yield return UIController
                    .DisplayError(
                        LanguagePack.GetLable("Error_InvalidPhoneNumber"),
                        IconPack.GetIcon("UnkownPhone"));
                break;

            // خطا در اتصال به سرور
            case AccountManager.SendCodeResultEnum.NetworkError:
                yield return UIController
                    .DisplayError(
                        LanguagePack.GetLable("Error_InternetAccess"),
                        IconPack.GetIcon("NetworkError"));
                break;

            // سرویس پیام کوتاه در دسترس نیست
            case AccountManager.SendCodeResultEnum.SmsServiceError:
                yield return UIController
                    .DisplayError(
                        LanguagePack.GetLable("Error_SmsService"),
                        IconPack.GetIcon("ServiceError"));
                break;

            // ارسال موفقیت آمیر کد به صورت پیام کوتاه
            case AccountManager.SendCodeResultEnum.Success:
                yield return UIController
                    .InputCodeWindow.ShowWaitForCloseHide();

                if (AccountManager.Instance.IsConnected)
                {
                    Close();
                    yield break;
                }
                break;
        }

        // Show phone number window in case of error
        yield return Show();
    }

}