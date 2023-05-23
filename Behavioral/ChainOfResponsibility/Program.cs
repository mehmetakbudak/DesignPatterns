/*
    Chain of Responsibility yani sorumluluk zinciri tasarım kalıbı, efektif ve gevşek bağlı bir şekilde onay akışı yapmamızı sağlar. Chain of responsibility pattern'i en iyi örnek üzerinden anlayabiliriz. Bir şirket çalışanı olarak şirketten ihtiyaç avansı talebinde bulunmanız gerekiyor. 10000 Tl civarı bir miktar talep ediyorsunuz ve bu talep bir onay akışına giriş yapıyor. 2000 TL'ye kadar olan talepleri yöneticiniz handle ederken üstü miktarlar grup yöneticinize onaya gidiyor. Grup yöneticisi 5000 TL'ye kadar olan talepleri handle ederken üstü talepler direktör onayına gidiyor ve direktör uygun bulursa onaylıyor. Bu gibi onay akışını özellikle de onaycıları if-else bloklarıyla kodlamaya kalkarsanız tamamen değişime kapalı ve tight-coupled bir sistem elde etmiş olursunuz ve yeni bir onaycının sisteme dahil olması durumunda  ya da onaycıların sırasının değişmesi gerektiğinde kod değişikliğine gitmeniz gerekebiliyor. Bu gibi onay akışı case'lerini chain of responsibility pattern ile efektif olarak kodlayabiliriz.  
 
    Chain of responsibility pattern bize her onaycının kendi içerisinde kendinden bir sonraki onaycıyı tutması gerektiğini ve bu tutacağı onaycılara gevşek bağlı olmamız gerektiğini yani bunları inject etmemiz gerektiğini söyler.

    Aşağıda Calisan class'ında parent olmak üzere abstract bir Calisan class'ı tanımlanmıştır. Bu Calisan class'ı kendi içerisinde kendinden sonraki onaycı bilgisini NextApprover olarak tutmaktadır.
 */

using System;

// Aşağıdaki onay akışı için oluşturduğumuz onaycıları bir diğer onaycıya nextApprover olarak inject ediyoruz. İşin güzelliği de tam burada. Hiç biri birbirine sıkı sıkıya bağlı değil. Akış çok kolay bir şekilde değiştirilebilir durumda. 
var withDraw = new WithDraw("1234", 134000, "TRY", "TR681223154132432141412");

Calisan sorumlu = new Sorumlu();
Calisan yonetici = new Yonetici();
Calisan grupYoneticisi = new GrupYoneticisi();
Calisan direktor = new Direktor();

sorumlu.SiradakiOnayciyiSetEt(yonetici);
yonetici.SiradakiOnayciyiSetEt(grupYoneticisi);
grupYoneticisi.SiradakiOnayciyiSetEt(direktor);

sorumlu.ProcessRequest(withDraw);

Console.ReadKey();



public abstract class Calisan
{
    protected Calisan SiradakiOnayci { get; set; }

    public void SiradakiOnayciyiSetEt(Calisan calisan)
    {
        this.SiradakiOnayci = calisan;
    }

    public abstract void ProcessRequest(WithDraw req);
}

// Direktor class'ı onay akışından son onaycıdır. Dolayısıyla kendinden sonraki bir onaycı yoktur ve 750000 TL'ye kadar talepleri handle eder. 
public class Direktor : Calisan
{
    public override void ProcessRequest(WithDraw req)
    {
        if (req.Amount <= 750000)
        {
            Console.WriteLine($"{this.GetType().Name} tarafından para çekme işlemi onaylandı. #{req.Amount} TL");
        }
        else
        {
            throw new Exception($"Limit banka günlük işlem limitini aştığından para çekme işlemi #{req.Amount} TL onaylanmadı.");
        }
    }
}

// Grup yöneticisi 150000 TL'ye kadar olan talepleri handle edebilir. Üstü talepleri kendinden sonraki onaycıya gönderir. 
public class GrupYoneticisi : Calisan
{
    public override void ProcessRequest(WithDraw req)
    {
        if (req.Amount <= 150000)
        {
            Console.WriteLine($"{this.GetType().Name} tarafından para çekme işlemi onaylandı. #{req.Amount} TL");
        }
        else if (SiradakiOnayci != null)
        {
            Console.WriteLine($"{req.Amount} TL işlem tutarı {this.GetType().Name} max. limitini aştığından işlem yöneticiye gönderildi.");

            SiradakiOnayci.ProcessRequest(req);
        }
    }
}

// Yönetici 70000 TL'ye kadar olan talepleri handle edebilir. Üstü talepleri kendinden sonraki onaycıya gönderir.
public class Yonetici : Calisan
{
    public override void ProcessRequest(WithDraw req)
    {
        if (req.Amount <= 70000)
        {
            Console.WriteLine($"{this.GetType().Name} tarafından para çekme işlemi onaylandı. #{req.Amount} TL");
        }
        else if (SiradakiOnayci != null)
        {
            Console.WriteLine($"{req.Amount} TL işlem tutarı {this.GetType().Name} max. limitini aştığından işlem yöneticiye gönderildi.");

            SiradakiOnayci.ProcessRequest(req);
        }
    }
}

// Sorumlu 40000 TL'ye kadar olan talepleri handle edebilir. Üstü talepleri kendinden sonraki onaycıya gönderir.
public class Sorumlu : Calisan
{
    public override void ProcessRequest(WithDraw req)
    {
        if (req.Amount <= 40000)
        {

        }
        else if (this.SiradakiOnayci != null)
        {
            Console.WriteLine($"{req.Amount} TL işlem tutarı {this.GetType().Name} max. limitini aştığından işlem yöneticiye gönderildi.");

            SiradakiOnayci.ProcessRequest(req);
        }
    }
}

public class WithDraw
{
    public string CustomerId { get; }
    public decimal Amount { get; }
    public string CurrencyType { get; set; }
    public string SourceAccountId { get; set; }

    public WithDraw(string customerId, decimal amount, string currencyType, string sourceAccountId)
    {
        CustomerId = customerId;
        Amount = amount;
        CurrencyType = currencyType;
        SourceAccountId = sourceAccountId;
    }
}