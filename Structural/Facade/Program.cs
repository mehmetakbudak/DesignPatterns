/*
    Bir alt sistemin parçalarını oluşturan class'ları istemciden soyutlayarak kullanımı daha da kolaylaştırmak için tasarlanmıştır. Mimarisel açıdan ise karmaşık ve detaylı bir sistemi organize eden ve bir bütün olarak client'lara sunan yapıdır. Anlayacağınız karmaşık ve detaylı olarak nitelendirdiğimiz bu sistemi bir alt sistem olarak varsayarsak eğer bu sistemi kullanacak client'lara daha basit bir arayüz sağlamak ve alt sistemleri bu arayüze organize bir şekilde dahil etmek ve bu alt sistemlerin sağlıklı çalışabilmesi için bu arayüz çatısı altında işin algoritmasına uygun işlev sergilemek istersek Facade design patterni kullanmaktayız.

    Burada bilmemiz gereken durum, alt sistem içerisinde bulunan sınıfların birbirinden bağımsız olmasıdır. Ayriyeten Facade sınıfından da bağımsız bir şekilde çalışabilmektedirler. Facade bizim class'larımızı içermek zorundadır ve operasyonu yaparken onlara ait fonksiyonellikleri kullanması gereklidir.

    Örneğimizde banka, kredi, merkezbanka class'ları birlikte müşteri bilgisine farklı açılardan bakarak sonuç olarak müşterinin kredi kullanıp kullanamayacağına karar veren bir sistemdir. Aşağıdaki senaryo facade tasarım kalıbını görmek için yazılmış basit bir senaryodur. Gerçek senaryolarda facade'nin kullandığı sistem class'ları çoğunlukla library'lerden gelmektedir.  
 */


using System;

Facade facade = new Facade();
facade.KrediKullan(new Musteri { Ad = "Mehmet", MusteriNumarasi = 1213131, TcNo = "123232432423" }, 10000);


// Banka class'ı
class Banka
{
    public bool KrediyiKullan(Musteri musteri, decimal talepEdilenMiktar)
    {
        return true;
    }
}

// kredi class'ı => kredi kullanma durumunu sorgular
class Kredi
{
    public bool KrediKullanmaDurumu(Musteri musteri)
    {
        return true;
    }
}

// MerkezBanka class'ı => müşteri kara listede mi değil mi sorgulayan class
class MerkezBanka
{
    public bool KaraListeKontrol(string TCNo)
    {
        return false;
    }
}

// facade class'ı => içerisinde müşterinin istediği fonksiyonelliği gerisinde bulunan sistemdeki parçaları organize ederek tek elden sunar. 
class Facade
{
    private Banka _banka;
    private Kredi _kredi;
    private MerkezBanka _merkezBanka;

    public Facade()
    {
        _banka = new Banka();
        _kredi = new Kredi();
        _merkezBanka = new MerkezBanka();
    }

    public void KrediKullan(Musteri musteri, decimal talepEdilenTutar)
    {
        if (!_merkezBanka.KaraListeKontrol(musteri.TcNo) && _kredi.KrediKullanmaDurumu(musteri))
        {
            _banka.KrediyiKullan(musteri, talepEdilenTutar);
            Console.WriteLine("Krediyi kullandırdık.");
        }
    }
}


// yardımcı class
public class Musteri
{
    public int MusteriNumarasi { get; set; }
    public string TcNo { get; set; }
    public string Ad { get; set; }
}