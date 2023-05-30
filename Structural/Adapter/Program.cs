/*
    Adapter tasarım kalıbı bir sistem parçasının var olan başka bir sisteme adapte edilmesini ve sistem içerisinde kullanılabilmesini sağlayan bir kalıptır. Aşağıda bununla ilgili örnekte IError interface'inden türeyen DbError ve ServiceError class'larından oluşan bir class grubu yani sistem ve bu sisteme dahil olması gereken Fax class'ı ve bunu gerçekleştiren FaxAdapter class'ı söz konusudur. Yazdığımız adapter class Fax class'ının IError sistem grubuna entegre olmasını sağlamaktadır.
 */


using System;

Fax fax = new Fax { ErrorDescription = "Cevap gelmiyor.", FaxErrorCode = 4000 };

IError[] errors =
{
    new DbError { Description = "Bağlantı sağlanamadı.", ErrorNumber = 100 },
    new DbError { Description = "Sorgulama sağlanamadı.", ErrorNumber = 101 },
    new ServiceError { Description = "Yetki sağlanamadı.", ErrorNumber = 300 },
    new FaxAdapter(fax)
};

foreach (var error in errors)
{
    error.SendMail();
}

Console.ReadKey();


// IError adında bir interface imiz var ve bu interface kendinden türeyen class'lar için bir grup oluşturacak. 
interface IError
{
    int ErrorNumber { get; set; }
    string Description { get; set; }
    void SendMail();
}

// Aşağıda DbError adında bir class'ımız var ve bu class IError'dan türemiş vaziyette olup interface'i implemente etmektedir.
public class DbError : IError
{
    private int _errorNumber;
    private string _description;

    public int ErrorNumber
    {
        get { return _errorNumber; }
        set { _errorNumber = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public void SendMail()
    {
        Console.WriteLine($"{ErrorNumber} {Description} -> Db hatası gönderildi.");
    }
}


// Aşağıda ServiceError adında bir class'îmız var ve bu class IError'dan türemiş vaziyette olup interface'i implemente etmektedir.
public class ServiceError : IError
{
    private int _errorNumber;
    private string _description;

    public int ErrorNumber
    {
        get { return _errorNumber; }
        set { _errorNumber = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public void SendMail()
    {
        Console.WriteLine($"{ErrorNumber} {Description} servis hatası gönderildi.");
    }
}

// yukarıdaki iki class aynı interface'den türemiş bir grup oluşturmuş vaziyette. Yani herhangi bir fonksiyonda ortak interface üzerinden değerlendirilebilirler. Aşağıda Fax adında bir class bulunmakta ve bu class diğer class grubundan farklı bir yapıdadır. Diğer iki class Error class'ıdır. Bu class ise fax işleminden sorumludur. Ancak fax işleminin içerisinde oluşabilecek Error'lar için ErrorCode ve ErrorDescription gibi property'leri de içermektedir. Yapı olarak aynı inerface'den türemefdikleri için aynı fonksiyonda interface üzerinden birlikte ele alınamazlar.
class Fax
{
    public int FaxErrorCode { get; set; }
    public string ErrorDescription { get; set; }

    // fax yolla
    void Send()
    {

    }

    // fax al
    void Get()
    {

    }
}

// Ancak aynı anda ele alınmak istenildikleri durumlar da olabilir. Örneğin yapıya dahil tüm error'lar bir kerede değerlendirilmek istenildiğinde ve buna fax ile ilgili error'lar da dahilse bu grup dışındaki class'ında diğer error grubuna dahil olması yani adapte olması gerekir. Adapter design pattern de tam burada devreye girmektedir.
// Yapmamız gereken şey FaxAdapter isminde bir adapter class'ı tanımlamak ve bunu IError 'dan türetmek. Böylece FaxAdapter class'ımızdiğer error grubuna dahil olabilecek. FaxAdapter class'ımıza orjinal Fax sınıfımızdan bir nesne üreterek IError'dan kalıttığımız metotlar içerisinde ilgili fax class'ı metotlarını çağırırız. Aşağıda da görüldüğü gibi IError'dan gelen ErrorNumber orjinal fax class'ındaki FaxErrorCode'a, yine IError'dan gelen Description property'si de orjinal fax class'ındaki ErrorDescription'a aracılık etmektedir. Yani aslında FaxAdapter class'ı Fax class'ı IError interface'inden türeyen gruba adapte olabilsin diye aracılık eder.
class FaxAdapter : IError
{
    private Fax _fax;
    public FaxAdapter(Fax fax)
    {
        _fax = fax;
    }

    public int ErrorNumber
    {
        get { return _fax.FaxErrorCode; }
        set { _fax.FaxErrorCode = value; }
    }

    public string Description
    {
        get { return _fax.ErrorDescription; }
        set { _fax.ErrorDescription = value; }
    }

    public void SendMail()
    {
        Console.WriteLine($"{ErrorNumber} {Description} fax hatası alındı.");
    }
}