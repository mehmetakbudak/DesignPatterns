/*
 Mediator design pattern'ini birbiriyle ilişkili eş görevli bir grup nesneyi tek merkezden yönetmek ve aralarında gevşek bağlı (loosely coupled) sistemler kurmak istediğimizde kullanabiliriz. 
 
 Örneğin; birbiriyle konuşması gereken bir grup nesnemiz olduğunu varsayalım. Genelde bu varsayım için iki klasik örnek vardır: Chatbot ve hava yolu örneği.
 
 Chatbot örneği üzerinden gidersek birbiriyle konuşması gereken bir grup nesnemiz chatleşecek olan kişileri temsil eden nesneler, mediator class'ımızda bi kişilerin bir arada bulunup birbiriyle ilişki kurduğu chatroomdur. Normal şartlarda chat grupları chat leşecek kişiler vs için ayrı nesneler oluşturulduğunda bu kişileri birbiriyle konuşturabilmek için birbirine referans vermek gerekecektir. Ancak bu class'ların birbirine sıkı sıkıya bağlı olması(tightly coupling) demektir ve istemediğimiz bir durumdur. Sistemlerimizi olabldiğince gevşek bağlı inşa etmeliyiz. Chat örneği için bütün kullanıcıların birbiriyle olan ilişkisini ve mesajlaşmasını, kullanıcıları birbirine dokundurmadan bir ara katman yani mediator class üzerinden yönetiriz.  
 
 Sonuç olarak, mediator design pattern'ın bize söylediği şey, bu birbiriyle ilişki içinde olması gereken nesneleri ki bunlar birbiriyle chat'leşecek user'lar olabilir. tek bir merkezden (mediator class'ta) birbiriyle konuşturun. Böylece nesneler arasında gevşek bağlı sistemler kurmuş olursunuz ve karmaşıklığı minimuma indirirsiniz. 

 Mediator pattern ini kullanabileceğiniz case lere örnek verecek olursak;

 - birbirine gönderme yapan interaction içerisinde olan birden fazla object iniz varsa 
 - bu ilişkileri farklı bir katmanda yönetmek istiyorsanız

 Bu sayede nesneler birbirine referans vermez ve uzun vadede genişletilebilir bakımı kolay bir kod oluşmuş olur. Ancak mediator design pattern'inin faydaları olduğu gibi endişe veren noktaları da vardır. Mesela bu gibi kritik işlemleri tek bir mediator class ı üzerinden yönetmek bize single point of failure yani tek nokta sıkıntısı yaşatabilir. Yani mediator class ındaki bir sıkıntı tüm sistemi çökertir.
 */


// Katılımcı class'ı  collegues yani senaryodaki user'ların parent class'ıdır. Mesaj gönderme ve mesaj alma özellikleri parent class üzerinden yönetilir. Mesaj gönderilirken direk diğer user'a göndermek yerine mediator üzerinden gönderilir.

using System;
using System.Collections.Generic;

ConcreteMediator sohbetOdasi = new ConcreteMediator();

Katilimci Can = new Katilimci("Can");
Katilimci Canan = new Katilimci("Canan");
Katilimci Baris = new Katilimci("Barış");
Katilimci Ahmet = new Katilimci("Ahmet");
Katilimci Selvi = new Katilimci("Selvi");

sohbetOdasi.KayitOl(Can);
sohbetOdasi.KayitOl(Canan);
sohbetOdasi.KayitOl(Baris);
sohbetOdasi.KayitOl(Ahmet);
sohbetOdasi.KayitOl(Selvi);

Canan.MesajGonder("Can", "Selam Can. Yemeğe çıkacak mısın?");
Baris.MesajGonder("Ahmet", "İstediğim evraklar hazır mı?");
Canan.MesajGonder("Ahmet", "Toplantı saat 3'te");
Ahmet.MesajGonder("Barış", "İstediğin evraklar hazır.");

public class Katilimci
{
    public string Ad { get; set; }
    public ConcreteMediator ConcreteMediator { get; set; }
    public Katilimci(string ad)
    {
        this.Ad = ad;
    }

    public void MesajGonder(string kime, string mesaj)
    {
        ConcreteMediator.MesajGonder(Ad, kime, mesaj);
    }

    public virtual void MesajAlici(string kimden, string mesaj)
    {
        Console.WriteLine($"{kimden} => {Ad} : '{mesaj}'");
    }
}

// ConcreteColleague1 => birinci concrete colleague class'ıdır. Yani bir gruba ait chat user'ı temsil eder. Mesaj alma özelliğine sahiptir ki bunu da parent class'ı olan Katilimci class'ının MesajAlici metodu üzerinden yapar. 
class ConcreteColleague1 : Katilimci
{
    public ConcreteColleague1(string ad) : base(ad)
    {
    }

    public override void MesajAlici(string kimden, string mesaj)
    {
        Console.WriteLine("Kime => ConcreteColleague1: ");
        base.MesajAlici(kimden, mesaj);
    }
}

// ConcreteColleague2 ikinci concrete class'tır. Yani diğer bir gruba ait chat user'ı temsil eder. Mesaj alma özelliğine sahiptir ki bunu da parent class olan Katilimci class'ının MesajAlici metodu üzerinden yapar.      
class ConcreteColleague2 : Katilimci
{
    public ConcreteColleague2(string ad) : base(ad)
    {
    }

    public override void MesajAlici(string kimden, string mesaj)
    {
        Console.WriteLine("Kime => ConcreteColleague2: ");
        base.MesajAlici(kimden, mesaj);
    }
}

// mediator class'ımız, içerisinde birbiriyle haberleşecek tüm nesneleri Dictionary olarak tutar. Yani senaryo odaklı düşünürsek siz bir chat user'ısınız ve chatroom'a register oldunuz, chatroom sizi de dahil ederek chatroom da kaç kişi varsa bunu bilir. Bunu da kodda KayitOl() metodu üzerinden yapar. Bunun dışında MesajGonder() metodumuz vardır. Bu metod üzerinden user'lar birbirine mesaj gönderirler. Yani user'lar birbiriyle haberleşecekse bunu mediator'un MesajGonder() metodu üzerinden yaparlar. Birbirlerinde direk bağlı değillerdir. Mediator'un MesajGonder() metodu da mesaj alır ve uygun alıcıya gönderir.

public class ConcreteMediator : Mediator
{
    private Dictionary<string, Katilimci> _katilimcilar = new Dictionary<string, Katilimci>();

    public override void KayitOl(Katilimci katilimci)
    {
        if (!_katilimcilar.ContainsValue(katilimci))
        {
            _katilimcilar[katilimci.Ad] = katilimci;
        }
        katilimci.ConcreteMediator = this;
    }

    public override void MesajGonder(string kimden, string kime, string mesaj)
    {
        Katilimci katilimci = _katilimcilar[kime];

        if (katilimci != null)
        {
            katilimci.MesajAlici(kimden, mesaj);
        }
    }
}

public abstract class Mediator
{
    public abstract void KayitOl(Katilimci katilimci);
    public abstract void MesajGonder(string kimden, string kime, string mesaj);
}