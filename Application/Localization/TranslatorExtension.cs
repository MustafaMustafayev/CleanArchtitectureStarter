namespace Application.Localization;

public static class TranslatorExtension
{
    public static string Translate(this EMessages message)
    {
        return MsgResource.ResourceManager.GetString(message.ToString())
               ?? "Message key could not find";
    }
}