/*
    Bir fonksiyonun birden fazla yapılış şekli olduğu takdirde, bu fonksiyonelliği farklı versiyonlarıyla kullanmak istendiğinde kullanılabilecek bir design pattern'dir. 
    
    Aynı işi farklı şekillerde yapan birden fazla concrete strategy sınıfımız olduğundan bunları bir strategy class üzerinden client a sunmak, strategy class ına da bu concrete tiplere ait ortak ata olan interface i vermek, ilerleyen zamanlarda bu concrete tiplere yeni birisi daha eklendiğinde işimizi kolaylaştıracak, bu durumda tek yapmamız gereken bu concrete tipi ortak interface den türetmek yeterli olacaktır. Strategy design pattern ın da tam olarak yaptığı budur.

    Aşağıda biri XMLSerialize yapmak diğeri de BinarySerialize yapmak üzere iki adet concrete tipimiz var ve bunların ikisi de ISerializable interface inden türetilmiştir. Yukarıda da bahsettiğim gibi iki concrete classımız serialize ve deserialize işlemlerini yani temelde aynı işlemleri iki farklı şekilde yapan sınıftır. Birisi xml serialize-deserialize ederken diğeri binary serialize-deserialize eder. Dolayısıyla bu logic üzerinden strategy pattern kullanılabilir. 
 */

// Client tarafından yapılması gereken tek şey strategy class ımız olan Serializer sınıfına ilgili parametreningönderilmesi ve çalıştırılmasıdır. İlerleyen zamanlarda Json serializer gibi bir yapının da yapıya eklenmek istendiğini düşünürsek yapılması gereken tek şey bunu ISerializable den kalıtmak olacak. Bu şekilde yeni concrete sınıfımız sisteme entegre olmuş olacak.


Serializer serializer = new Serializer(new XmlSerializer());
serializer.Serialize("");
serializer.Deserialize("");

serializer = new Serializer(new BinarySerializer());
serializer.Serialize("");
serializer.Deserialize("");


//base interface
interface ISerializable
{
    void Serialize(string str);
    void Deserialize(string str);
}

// concrete tip 1
class XmlSerializer : ISerializable
{
    public void Deserialize(string str)
    {
        Console.WriteLine("xml için ters serileştirme gerçekleşti.");
    }

    public void Serialize(string str)
    {
        Console.WriteLine("xml için serileştirme gerçekleşti.");
    }
}

// concrete tip 2
class BinarySerializer : ISerializable
{
    public void Deserialize(string str)
    {
        Console.WriteLine("binary için ters serileştirme gerçekleşti.");
    }

    public void Serialize(string str)
    {
        Console.WriteLine("binary için serileşme gerçekleşti.");
    }
}

// Serializer sınıfı bizim strategy sınıfımızdır. Client tamamıyla bu class ile iletişime geçecek ve gönderilen argumanlar bu sınıf üzerinden değerlendirilecektir.

class Serializer
{
    ISerializable _serializable;

    public Serializer(ISerializable serializable)
    {
        _serializable = serializable;
    }

    public void Serialize(string str)
    {
        _serializable.Serialize(str);
    }

    public void Deserialize(string str)
    {
        _serializable.Deserialize(str);
    }
}
