# TmpMail
Documentation: https://github.com/supertoha/TmpMail

Get API Key: https://rapidapi.com/supertoha/api/temporary-email-service

NuGet **TmpMail** 

![NuGet Version](https://img.shields.io/nuget/vpre/TmpMail)
![NuGet Downloads](https://img.shields.io/nuget/dt/TmpMail)



### Features:
* Create mailbox
* Get letters
* Get attached files
* Remove mailbox
* Remove letter
* Send emails
* Reply 
* Get mailboxes

### Sample:
```Csharp
internal class Program
{
    private async static Task Main(string[] args)
    {
        using var tmpMail = new TmpMail.TmpMail("<YOUR API KEY>"); // https://rapidapi.com/supertoha/api/temporary-email-service
        var mailBox = await tmpMail.CreateMailboxAsync();
        Console.WriteLine($"Mailbox is created {mailBox.Email}");

        Console.WriteLine("Press any key to get emails");
        Console.ReadKey();

        // print all emails
        var emails = await mailBox.GetEmailsAsync();
        foreach (var email in emails)
        {
            Console.WriteLine(email);
        }

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();

        // delete mailbox
        await mailBox.DeleteAsync();
    }
}
```
