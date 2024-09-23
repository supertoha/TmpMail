# TmpMail

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
