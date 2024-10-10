using TmpMail;

internal class Program
{
    private async static Task Main(string[] args)
    {
        using var tmpMail = new TmpMail.TmpMail("<YOUR API KEY>"); // https://rapidapi.com/supertoha/api/temporary-email-service
        var mailBox = await tmpMail.CreateMailboxAsync();
        Console.WriteLine($"Mailbox is created {mailBox.Email}");

        //Console.WriteLine("Press any key to send email");
        //Console.ReadKey();
        //await mailBox.SendEmail("Hi Bob! \nHow are you?", "Hello", "your.email@email.com");

        Console.WriteLine("Press any key to get emails");
        Console.ReadKey();

        // print all emails
        var emails = await mailBox.GetEmailsAsync();
        foreach (var email in emails)
        {
            Console.WriteLine(email);

            foreach (var attachment in email.Attachments)
                Console.WriteLine(attachment.Filename);
        }

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();

        // delete mailbox
        await mailBox.DeleteAsync();
    }
}