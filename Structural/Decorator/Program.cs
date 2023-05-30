/*
    Decorator tasarım deseninin amacı nesnelere dinamik olarak özellik eklemektir ve nesne kendisi de eklenen özelliklerden habersiz ve ayrı bir konumda olmalıdır. Yani kodun belli kısımlarında nesnelere belli özellikler kazandırmak istiyorsak ve bunu nesnenin kendî class'ından ayrıştırılmış bir şekilde yapmak istiyorsak decorator tasarım desenini kullanmalıyız.

    --- Decorator tasarım deseninin en önemli özellikleri ---
    - Esas nesne dekore edildiğinin farkında değildir. Yani dekoratör ile eklenen özellikler aslında kendi class'ı içerisinde barındırdığı özellikler değildir. 
    - Esas nesnenin class'ı tüm gerekli gereksiz opsiyonları içerisinde barındıran büyük bir class halinden çıkmış olur.
    - Tüm decorator class'ları birbirinden bağımsızdır.
    - Decorator class'ları kendi arasında combine edilip eşleştirilebilir.

    --- Decorator tasarım deseninin ana oyuncuları ---
    - Component => Üst sınıfların uygulaması için ortak bir arayüz. Bu arayüze tanımlanan işlemler daha sonra ConcreteDecorator sınıfları tarafından değiştirilen tanımlamalardır.
    - ConcreteComponent => Temel davranışın uygulandığı sınıftır. ConcreteDecorator sayesinde değiştirilecektir.
    - BaseDecorator =>  Component arayüzünü uygular ve bu arayüzü uygulayan yapının referansını da barındırır.
    - ConcreteDecorator => Yeni davranışların tanımlandığı sınıftır. BaseDecorator sınıfından türer.
 */

using System;

IDataSource dataSource = new FileDataSource("data.sql");
IDataSource compressedDataSource = new CompressionDecorator(dataSource);

compressedDataSource.ReadData();
compressedDataSource.WriteData(new object());

Console.ReadKey();

// Üst sınıfların uygulayacağı arayüzdür. Yapılacak temel işlemler tanımlanmıştır. UML diyagramındaki component arayüzüne denk gelmektedir.
interface IDataSource
{
    string GetFileName();
    void WriteData(object data);
    void ReadData();
}

// Temel işlemlerin tanımlandığı IDataSource arayüzünü uygulayan sınıftır. ConcreteComponent'e denk gelmektedir.
class FileDataSource : IDataSource
{
    private string _fileName;

    public FileDataSource(string fileName)
    {
        _fileName = fileName;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public void ReadData()
    {
        Console.WriteLine($"{_fileName} readed.");
    }

    public void WriteData(object data)
    {
        Console.WriteLine($"data was written to {_fileName}.");
    }
}

//Component(IDataSource) arayüzünü uygular. Constructor sayesinde özellikleri değiştirilmesi istenen nesnenin referansı tutulur. Değiştirilmesini istediğimiz metotları abstract anahtar kelimesi ile işaretledik.  BaseDecorator yapısına denk gelmektedir.
abstract class BaseDataSourceDecorator : IDataSource
{
    protected IDataSource dataSource;

    public BaseDataSourceDecorator(IDataSource dataSource)
    {
        this.dataSource = dataSource;
    }

    public abstract string GetFileName();

    public abstract void ReadData();

    public abstract void WriteData(object data);
}

class CompressionDecorator : BaseDataSourceDecorator
{
    public CompressionDecorator(IDataSource dataSource) : base(dataSource)
    {
    }

    public override string GetFileName()
    {
        return base.dataSource.GetFileName();
    }

    public override void ReadData()
    {
        // Veriyi okurken referansını tuttuğumuz sınıfın kendi davranışını sergilemesini istiyoruz.
        base.dataSource.ReadData();
    }

    public override void WriteData(object data)
    {
        //
        Console.WriteLine("Data compressed.");
        //
        Console.WriteLine($"Compressed data was written to {base.dataSource.GetFileName()}.");
    }
}
