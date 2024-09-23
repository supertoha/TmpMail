# TmpMail
![logo_128x128](https://github.com/user-attachments/assets/7b1c879b-1365-49be-9e54-b8f9ae9f1d49)


NuGet\Install-Package TmpMail 

```
internal class Program
{
    private async static Task Main(string[] args)
    {
        using var tmpMail = new TmpMail.TmpMail("<YOUR API KEY>"); // https://rapidapi.com/supertoha/api/temporary-email-service
        var mailBox = await tmpMail.CreateMailboxAsync();
        while (true)
        {
            await Task.Delay(5000);
            var emails = await mailBox.GetEmailsAsync(true);
            foreach(var email in emails)
            {
                Console.WriteLine(email);
            }
        }        

    }
}
```
