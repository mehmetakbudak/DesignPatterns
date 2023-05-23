/*
    Memento tasarım kalıbı ile varlıkların initial stateleri yani o anki mevcut konumları saklanarak herhangi bir zamanda bir önceki hallerine döndürülmek istenen durumlarda kullanılır.

    Originator => Bu sınıf durumu tutulacak olan nesnemiz oluyor. eski veya yeni halini tutmamızı sağlayacak metotlar burada yer alacak. 
    
    Memento => Bu sınıf ise, asıl nesnemizin istediğimiz alanlarını tutan sınıftır.
 
    Caretaker(Bekçi) => Geri dönüş adımlarımızı memento tipinde tutacak olan sınıftır.

    Aşağıdaki senaryo bir oyun üzerinde. Oyunda bulunan gezegenler, oyunun belirli zamanlarında kaydedilen eski haline çevrilebilsin.
 */


using System;

GameWorld zulu = new GameWorld { Name = "Zulu", Population = 100000 };

Console.WriteLine(zulu.ToString());

GameWorldCareTaker taker = new GameWorldCareTaker();
taker.World = zulu.Save();
zulu.Population += 10;

Console.WriteLine(zulu.ToString());

zulu.Undo(taker.World); // eski dünyayı geri yükle

Console.WriteLine(zulu.ToString());

Console.ReadKey();



// originator class => saklamak istediğimiz gerçek nesnemizin sınıfı
class GameWorld
{
    public string Name { get; set; }

    public long Population { get; set; }

    // yeni bir GameWorldMomento nesnesi örnekleyip ona orginator class ına ait nesnenin ilgili özelliklerini atar.
    public GameWorldMomento Save()
    {
        return new GameWorldMomento
        {
            Name = this.Name,
            Population = this.Population
        };
    }

    public void Undo(GameWorldMomento model)
    {
        this.Name = model.Name;
        this.Population = model.Population;
    }

    public override string ToString()
    {
        return string.Format("{0} dünyasında {1} insan var.", Name, Population);
    }
}

// Memento class => T anında save etmek istediğimiz nesnenin saklanacağı sınıftır.Hangi özellikleri saklamak istersek onlara uygun property ler tanımlamamız yeterli olacaktır.
class GameWorldMomento
{
    public string Name { get; set; }
    public long Population { get; set; }
}

// CareTaker sınıfı => momento class'ını güvenli bir şekilde saklar ama üyeleri üzerinde herhangi bir değişiklik yapmaz.
class GameWorldCareTaker
{
    public GameWorldMomento World { get; set; } //momento'yu döner
}


