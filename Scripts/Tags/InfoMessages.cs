using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class InfoMessages
{
       

    public enum MESSAGE_ID
    {
        NONE = 0, START = 1, FINISH = 2, BEFORE_SELECTED = 3, TRUESELECTED = 4, WRONGSELECTED = 5, CHANGEKKDSCENE = 6, TRUECODE = 7, FALSECODE = 8,
        CARRABINER = 9, BRUSH = 10, KG = 11, TIMER = 12, ERROR = 13, GETONLIFT_ERROR = 14

    }
        
    static List<Message> messages;

    // if you need to increase the supported languages you need to change Language.cs
    static Message dummyMessage = null;     // to comply with the enum
    static Message startMessage;
    static Message finishMessage;
    static Message beforeSelected;
    static Message trueSelected;
    static Message wrongSelected;
    static Message changeKkdScene;
    static Message trueCode;
    static Message falseCode;
    static Message carabiner;
    static Message brush;
    static Message kG;
    static Message timer;
    static Message error;
    static Message grabState;
    static Message closeDoor;
    static Message pegasusTitle;
    static Message userNotVerified;
    static Message getonLiftError;

    static InfoMessages()
    { 
        FillMessages();

    }
    
    public static string GetMessage(MESSAGE_ID i)
    {
        return messages[(int)i].GetMessage();

    }

    static void FillMessages()
    {
        
        messages = new List<Message>();
        startMessage = new Message();
        finishMessage = new Message();
        beforeSelected = new Message();
        trueSelected = new Message();
        wrongSelected = new Message();
        changeKkdScene = new Message();
        trueCode = new Message();
        falseCode = new Message();
        carabiner = new Message();
        brush = new Message();
        kG = new Message();
        timer = new Message();
        error = new Message();
        grabState = new Message();
        closeDoor = new Message();
        pegasusTitle = new Message();
        userNotVerified = new Message();
        getonLiftError = new Message();

        startMessage.AddMessage("Sahnede kapsül oluşturuldu.");
        startMessage.AddMessage("Capsule was created on a stage");

        finishMessage.AddMessage("Çeki demirini kollarınızı kullanarak indiriniz.");
        finishMessage.AddMessage("Lower the drawbar with your arms.");

        beforeSelected.AddMessage("Karabineri yerleştirin.");
        beforeSelected.AddMessage("Place the carabiner.");

        trueSelected.AddMessage("Doğru KKD seçimi");
        trueSelected.AddMessage("Correct PPE selection");

        wrongSelected.AddMessage("Yanlış KKD seçimi");
        wrongSelected.AddMessage("Wrong PPE selection");

        changeKkdScene.AddMessage("Ana sahneye geçmek için holograma gidiniz.");
        changeKkdScene.AddMessage("Please proceed to the hologram to move to the main stage.");

        trueCode.AddMessage("Kodunuz kabul edildi.");
        trueCode.AddMessage("Your code is accepted.");

        falseCode.AddMessage("Kodunuz kabul edilmedi");
        falseCode.AddMessage("Your code is not accepted.");

        carabiner.AddMessage("Karabineri yerleştirmek için bırakınız.");
        carabiner.AddMessage("Release to insert the carabiner.");

        brush.AddMessage("Spreyi uçağın yüzeyine sıkabilirsiniz.");
        brush.AddMessage("You can spray onto the surface of the airplane.");

        kG.AddMessage("Almaya çalıştığınız bavul 23 kg'ın üstündedir. \n Çalışma arkadaşınız size yardım edecek. \n İki elinizi birden bavula değdiriniz.");
        kG.AddMessage("The suitcase you are trying to take is over 23 kg.\n Your colleague will help you. \n Touch the suitcase with both hands at once.");

        timer.AddMessage("");
        timer.AddMessage("");

        error.AddMessage("Bavulu beraber taşımanız gerekiyor.");
        error.AddMessage("You have to carry the luggage together.");

        grabState.AddMessage("Şimdi kapıyı açabilirsiniz.");
        grabState.AddMessage("Now, you can open the Door.");

        closeDoor.AddMessage("Kapıyı açmak için bütün nesneleri yerine yerleştiriniz.");
        closeDoor.AddMessage("To open the door, please place all objects back in their proper places");

        pegasusTitle.AddMessage("Pegasus'a hoşgeldiniz.");
        pegasusTitle.AddMessage("Welcome to Pegasus.");

        userNotVerified.AddMessage("Lütfen kodunuzu doğru giriniz.");
        userNotVerified.AddMessage("Please enter your code correctly.");

        getonLiftError.AddMessage("Karabineri ankraj noktasına bağlamadan binmek tehlikelidir.");
        getonLiftError.AddMessage("Boarding without attaching the carabiner to the anchor point is dangerous");


        // do this last
        AddMessagesToList();
    }

    /// <summary>
    /// Messages should be addded with respect to enum order
    /// </summary>
    static void AddMessagesToList()
    {
        messages.Add(dummyMessage);
        messages.Add(startMessage);
        messages.Add(finishMessage);
        messages.Add(beforeSelected);
        messages.Add(trueSelected);
        messages.Add(wrongSelected);
        messages.Add(changeKkdScene);
        messages.Add(trueCode);
        messages.Add(falseCode);
        messages.Add(carabiner);
        messages.Add(brush);
        messages.Add(kG);
        messages.Add(timer);
        messages.Add(error);
        messages.Add(grabState);
        messages.Add(closeDoor);
        messages.Add(pegasusTitle);
        messages.Add(userNotVerified);
    }

    public static bool NoneMessage(MESSAGE_ID message)
    {
        return message == MESSAGE_ID.NONE;
    }
}
