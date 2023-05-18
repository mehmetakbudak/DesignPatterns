/*
 Factory design pattern'den farkı ilişkisel olan birden fazla nesnenin üretimini tek bir arayüz tarafından değil her ürün ailesi için farklı bir arayüz tanımlayarak sağlamaktadır. Yani birden fazla ürün ailesi ile çalışmak zorunda kaldığımız durumlarda istemciyi bu yapılardan soyutlamak için kullanılır. Factory design pattern de ilişkisel olan birden fazla nesnenin üretimini ortak bir arayüz üzerinden tek bir sınıf üzerinden yapılacak bir talep ile gerçekleştirmek ve nesn üretim anında istemcinin üretilen nesneye bağımlılığını azaltmak hedeflenmiştir.

 */

using System;

Factory factory = new Factory(new MSSqlFactory());
factory.Start("select  * from Users");
Console.WriteLine("-------------------");
factory = new Factory(new OracleFactory());
factory.Start("select * from Products");

Console.ReadKey();

public abstract class Connection
{
    public abstract bool Connect();
    public abstract bool Disconnect();
    public abstract string State { get; } // readonly
}


public abstract class Command
{
    public abstract void Execute(string query);
}


public class OracleConnection : Connection
{
    public override string State => "Open";

    public override bool Connect()
    {
        Console.WriteLine("Oracle bağlantısı açılacak.");
        return true;
    }

    public override bool Disconnect()
    {
        Console.WriteLine("Oracle bağlantısı kapatılacak.");
        return true;
    }
}


public class MSSqlConncetion : Connection
{
    public override string State => "Open";

    public override bool Connect()
    {
        Console.WriteLine("MSSql bağlantısı açılacak.");
        return true;
    }

    public override bool Disconnect()
    {
        Console.WriteLine("MSSql bağlantısı kapatılacak.");
        return true;
    }
}

public class OracleCommand : Command
{
    public override void Execute(string query)
    {
        Console.WriteLine($"{query} sorgusu Oracle veritabanında çalıştırıldı.");
    }
}

public class MSSqlCommand : Command
{
    public override void Execute(string query)
    {
        Console.WriteLine($"{query} sorgusu MSSql veritabanında çalıştırıldı.");
    }
}

public abstract class DatabaseFactory
{
    public abstract Connection CreateConnection();
    public abstract Command CreateCommand();
}


public class OracleFactory : DatabaseFactory
{
    public override Command CreateCommand()
    {
        return new OracleCommand();
    }

    public override Connection CreateConnection()
    {
        return new OracleConnection();
    }
}

public class MSSqlFactory : DatabaseFactory
{
    public override Command CreateCommand()
    {
        return new MSSqlCommand();
    }

    public override Connection CreateConnection()
    {
        return new MSSqlConncetion();
    }
}

public class Factory
{
    private DatabaseFactory _databaseFactory;
    private Connection _connection;
    private Command _command;


    public Factory(DatabaseFactory databaseFactory)
    {
        _databaseFactory = databaseFactory;
        _command = _databaseFactory.CreateCommand();
        _connection = _databaseFactory.CreateConnection();
    }

    public void Start(string query)
    {
        if (_connection.State == "Open")
        {
            _connection.Connect();
            _command.Execute(query);
            _connection.Disconnect();
        }
    }
}
