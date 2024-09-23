
internal class Program
{
    private async static Task Main(string[] args)
    {
        using var tmpMail = new TmpMail.TmpMail("7b7d54fd5emsha7742e32c460b68p12549bjsn1fee2bd74737");
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
        

        Console.ReadKey();
    }
}