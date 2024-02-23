# Repositórios em Entity Framework

Já estamos avançando com os aprendizados em C#, e chegou a hora de ver como manipular dados de forma organizada. 🚀

Atualmente, existem muitas formas de se estruturar um programa. Entretanto, algo que todas essas arquiteturas têm em comum é o fato de separarem a camada de acesso aos dados da lógica de negócio.

> Para criarmos essa separação, geralmente utilizamos o Repository Pattern.

## Um padrão para manipular dados de forma organizada

Neste padrão de sistema, utilizamos classes ou componentes de modo a encapsular a lógica necessária para persistir os dados, os repositórios.

Anota aí 🖊: Além de evitar o código macarrônico, esse padrão facilita o gerenciamento do ciclo de vida de objetos e propõe um modelo simples para recuperá-los da base de dados.

Como podemos observar na imagem abaixo, o repositório serve como uma camada extra, centralizando as chamadas ao banco de dados em uma única camada.

![diferença de uso entre aplicação com e sem o padrão repository](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-diferen%C3%A7a%20de%20uso%20entre%20aplica%C3%A7%C3%A3o%20com%20e%20sem%20o%20padr%C3%A3o%20repository.jpeg)

A utilização de repositórios com o Repository Pattern gera um modelo simples para obter objetos e gerenciar seu ciclo de vida, permitindo a fácil substituição de ORMs e fonte de dados. Além disso, facilita os testes automatizados.

> O Mapeamento Relacional de Objetos, ou, no inglês, Object-Relational Mapping, é uma técnica que permite consultar e manipular dados de um banco de dados usando o paradigma de orientação a objetos.

De olho na dica👀: Como boa prática, a camada de acesso ao banco de dados sempre é a mais baixa de um programa, então é comum no dia a dia usarmos um framework para auxiliar na conexão e manipulação de dados que ocorrem nos repositórios.

O framework mais famoso para essa tarefa no universo .NET é o Entity Framework, um ORM open-source para aplicações .NET que trabalha com dados em alto nível de abstração.

O Entity Framework suporta uma infinidade de banco de dados, eliminando a necessidade de se preocupar com as diferentes sintaxes existentes.

E no Repository Pattern as consultas são feitas por meio do repositório, usando métodos de pesquisa para selecionar os dados que atendam ao critério especificado pela pessoa usuária. Normalmente, esse critério é o valor passado como parâmetro.

Esse repositório é responsável por realizar a consulta no banco de dados e retornar os dados requisitados, encapsulando a sintaxe de consulta e o mapeamento das tabelas.

Anota aí 🖊: Um repositório pode implementar uma variedade de operações como o básico CRUD, até consultas mais específicas. Você pode criar um repositório para cada model ou utilizar um para alterar várias models, dependendo da necessidade.

Agora vamos conhecer mais um pouco de como usar esse padrão para definir a arquitetura de um programa? Confira no vídeo abaixo.

## Quem não ama testes?

À medida que nosso programa vai ficando mais robusto e lidando com bases de dados externas, temos que criar alternativas para realizar nossos testes.

Essas alternativas podem ser um pouco tortuosas, e levar um tempo considerável para a implementação.

![surpresa](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-surpresa.gif)

Mas calma, antes de se desesperar, saiba que o padrão repository também nos auxilia nessa tarefa.

![ufa](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-ufa.gif)

Como podemos observar na imagem abaixo, a camada de abstração do `Repository` entre o `Controller` e o `DbContext` permite que, no momento em que realizarmos nossos testes, vamos ter apenas que nos preocupar em realizar mocks do nosso repositório, sem se preocupar com a infraestrutura de banco de dados.

![comparação entre app sem e com repository_testes](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-compara%C3%A7%C3%A3o%20entre%20app%20sem%20e%20com%20repository_testes.jpeg)

⚠️Aviso: Se não usássemos o `Repository Pattern`, teríamos um problemão para testar, pois as consultas ao banco de dados estariam no meio do código!

## Criando nossa primeira conexão com Entity Framework

Iremos começar criando uma webapi para criar nossos códigos. Entretanto, não iremos criar rotas para manipular todas as tabelas e iremos criar uma rota fixa de exemplo.

Copiar

```shell
1dotnet new webapi -o ApiSql
```

À primeira vista, criar uma conexão com o `Entity Framework` pode parecer um bicho de sete cabeças, mas a realidade não é tão assustadora assim.

![monstrinho](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-monstrinho.gif)

Mas, para conseguirmos nossa conexão, precisamos de um contexto que nos mostre quais `models` estão definidas na base e indique com qual base de dados devemos nos conectar.

O primeiro passo para estabelecermos conexão com o banco de dados é criar uma classe responsável por gerenciar essa conexão.

> O Entity Framework disponibiliza uma classe para esse gerenciamento: a `DbContext`, que deve ser herdada da seguinte forma:

Copiar

```csharp
1public class DatabaseContext : DbContext
2{
3
4}
```

O `DbContext` possibilita realizar as operações básicas de leitura, criação, atualização e exclusão em um banco de dados. Além disso, podemos realizar operações com agregações de tabelas.

Para indicarmos qual deve ser a base de dados com que a nossa aplicação deve se comunicar, precisaremos sobrescrever a função `OnConfiguring`, que recebe como parâmetro uma variável do tipo `DbContextOptionsBuilder`.

Copiar

```csharp
1public class DatabaseContext : DbContext
2{
3    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
4    {
5        optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=master;User=SA;Password=password123!;");
6    }
7}
```

Repare que, por meio da nossa variável `optionsBuilder`, estamos informando para nossa aplicação que a nossa conexão será realizada com um banco de dados do tipo **Sql Server** e passando a nossa string de conexão.

> De olho na dica👀: Entity Framework suporta vários tipos de banco de dados, para cada um deles temos uma biblioteca que nos fornece utilitários para a criação da conexão. Para adicionar a biblioteca referente ao Sql Server, basta rodar no console `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`.

Nossa string de conexão passa as informações necessárias para se criar uma conexão entre a aplicação e o banco de dados.

Agora vamos definir quais tabelas mapear na nossa aplicação. Digamos que temos o seguinte modelo de dados:

![modelo de banco de dados](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-modelo%20de%20banco%20de%20dados.jpeg)

Para nossa aplicação poder manipular essas tabelas, vamos precisar criar uma classe para cada e definir suas relações. Vamos marcar nossas chaves estrangeiras e primárias utilizando um recurso do C# chamado Data Annotations.

Copiar

```csharp
1public class Book
2{
3    [Key]
4    public int BookId { get; set; }
5    public string Title { get; set; }
6    public string Description { get; set; }
7    public string Genre { get; set; }
8    public int Year { get; set; }
9    public int Pages { get; set; }
10    [ForeignKey("AuthorId")]
11    public int? AuthorId { get; set; }
12     public Author Author { get; set; }
13     [ForeignKey("PublisherId")]
14    public int? PublisherId { get; set; }
15    public Publisher Publisher { get; set; }
16}
```

[Acesse o código completo aqui](https://github.com/tryber/csharp-codes/blob/S5-D2-L3-EX2/ApiSql/Models/Book.cs)

Copiar

```csharp
1public class Author
2{
3    [Key]
4    public int AuthorId { get; set; }
5    public string Name { get; set; }
6    public string Email { get; set; }
7    [InverseProperty("Author")]
8    public ICollection<Book> Books { get; set; }
9}
```

[Acesse o código completo aqui](https://github.com/tryber/csharp-codes/blob/S5-D2-L3-EX2/ApiSql/Models/Author.cs)

Copiar

```csharp
1public class Publisher
2{
3    [Key]
4    public int PublisherId { get; set; }
5    public string Name { get; set; }
6    [InverseProperty("Publisher")]
7    public ICollection<Book> Books { get; set; }
8}
```

[Acesse o código completo aqui](https://github.com/tryber/csharp-codes/blob/S5-D2-L3-EX2/ApiSql/Models/Publisher.cs)

- Para o Entity Framework, cada uma dessas classes é uma entidade.

> Anota aí 🖊: Uma entidade é um objeto que mapeia uma ou mais tabelas de um banco de dados. Para conseguir manipulá-las no programa, vamos precisar mapeá-las em nosso `context`.

Copiar

```csharp
1// Renomeado DatabaseContext para BookContext
2public class BookContext : DbContext
3{
4    public DbSet<Book> Books { get; set; }
5
6    public DbSet<Publisher> Publishers { get; set; }
7
8    public DbSet<Author> Authors { get; set; }
9
10    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
11    {
12        optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=master;User=SA;Password=password123!;");
13    }
14}
```

Dessa forma, nossa aplicação já sabe qual base de dados vamos utilizar, suas credenciais e quais tabelas queremos manipular.

> _Anota aí✏️: O nome `BookContext` é arbitrário e podemos utilizar o que melhor se encaixa com a aplicação que estamos criando_

## Repositórios

Agora que aprendemos como nos conectar com o banco de dados, vamos criar um repositório e concentrar todas as operações de banco de dados nele.

Um repositório é basicamente uma classe que contém nela métodos que encapsulam as regras de negócio do banco de dados.

Vamos então criar um repositório para as entidades `Book`, `Publisher` e `Author`.

Primeiro, precisamos que nossa classe tenha o atributo responsável pela comunicação com o banco de dados. Neste caso, o DbContext `BookRepository` criado anteriormente. E adicionamos este atributo por meio da injeção de dependências, da seguinte forma.

Copiar

```csharp
1public class BookRepository
2{
3  private readonly BookContext _context;
4
5  public BookRepository(BookContext context)
6  {
7    _context = context;
8  }
9}
```

E por fim, vamos disponibilizar o `BookContext` e o `BookRepository` como serviços para serem injetados por meio de injeção de dependências, fazemos isso com o seguinte trecho de código no arquivo `Program.cs`:

Copiar

```csharp
1builder.Services.AddDbContext<BookContext>();
2builder.Services.AddScoped<BookContext>();
3builder.Services.AddScoped<BookRepository>();
```

[Acesse o código completo aqui](https://github.com/tryber/csharp-codes/blob/S5-D2-L3-EX2/ApiSql/Program.cs)

Agora o nosso repositório já tem acesso ao contexto de banco de dados que foi injetado por meio de injeção de dependências.

Com isso, podemos começar a criar os métodos que serão responsáveis pelas alterações no banco de dados, utilizando para isso o contexto.

## Lendo registros com Repositórios

Vamos começar a explorar as possibilidades que temos em trabalhar com repositórios?

Relembrando🧠: O nosso `BookRepository` recebe em seu construtor uma instância de `DbContext`.

Copiar

```csharp
1public class BookRepository
2{
3    protected readonly DbContext _context;
4    public BookRepository(DbContext context)
5    {
6        _context = context;
7    }
8}
```

- Nossa tabela `Book` tem relação com as tabelas `Author` e `Publisher`, como podemos ver no diagrama abaixo:

![modelo de banco de dados](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-modelo%20de%20banco%20de%20dados.jpeg)

Queremos realizar uma consulta em nossa base de dados, retornando todos os livros. Para isso, vamos (I) chamar nossa conexão com o banco de dados, (II) informar a tabela que queremos consultar, e (III) dizer que o banco deve retornar essas informações em forma de lista:

Copiar

```csharp
1public class BookRepository
2{
3    protected readonly DbContext _context;
4    public BookRepository(DbContext context)
5    {
6        _context = context;
7    }
8
9    // Para realizar a consulta, adicionamos um método
10    // que realiza essa consulta em BookRepository
11    public List<Book?> GetBookList()
12    {
13        var query = _context.Books.ToList();
14
15        return query;
16    }
17}
```

- Ao chamarmos nosso método `GetBookList` e rodar o programa, nossa saída será:

Copiar

```console
1Id: 1 - Title: The Hobbit - Pages: 550 - Year: 2011
2Id: 2 - Title: Brave new World - Pages: 325 - Year: 1932
3Id: 3 - Title: The Divine Comedy - Pages: 811 - Year: 2013
```

Agora, vamos dizer que queremos recuperar o título do livro, o nome do autor e o nome da editora com o id 3:

Copiar

```csharp
1public class BookRepository
2{
3    protected readonly DbContext _context;
4    public BookRepository(DbContext context)
5    {
6        _context = context;
7    }
8
9    public List<Book?> GetBookList()
10    {
11        var query = _context.Books.ToList();
12
13        return query;
14    }
15
16    public Book GetById(int id)
17    {
18        return _context.Books.Where(e => e.BookId == id).Include(e => e.Author).Include(e => e.Publisher).First();
19    }
20}
```

- Ao chamarmos nosso método `GetById` e rodar o programa, nossa saída será:

Copiar

```console
1Id: 3 - Title: The Divine Comedy - Author: Dante Alighieri - Publisher: Paradise Publisher
```

Quando utilizamos o método `.Includes` na pesquisa, conseguimos incluir também as entidades `Author` e `Publisher` no resultado da consulta, não sendo necessário realizar uma nova consulta para buscar estas informações do livro.

## Status: em um relacionamento sério

Entre as nossas tabelas `Book`, `Publisher` e `Author`, temos um relacionamento conforme mostra a figura abaixo:

![modelo de banco de dados](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-modelo%20de%20banco%20de%20dados.jpeg)

Em nossa classe `Book`, definimos também o relacionamento entre `Publisher` e `Author` adicionando a anotação `ForeignKey` nas instâncias da classe `Author` e `Publisher`.

Copiar

```csharp
1public class Book
2{
3    [Key]
4    public int BookId { get; set; }
5    public string Title { get; set; }
6    public string Description { get; set; }
7    public string Genre { get; set; }
8    public int Year { get; set; }
9    public int Pages { get; set; }
10    public int? AuthorId { get; set; }
11    [ForeignKey("AuthorId")]
12    public Author Author { get; set; }
13    public int? PublisherId { get; set; }
14    [ForeignKey("PublisherId")]
15    public Publisher Publisher { get; set; }
16}
```

Em nosso context, precisamos agora fazer o mapeamento da classe `Book` e definir suas relações com as classes `Author` e `Publisher` na função `OnModelCreating` utilizando FluentAPI.

Copiar

```csharp
1public class BookContext : DbContext
2{
3    public DbSet<Book> Books { get; set; }
4    public DbSet<Author> Authors { get; set; }
5    public DbSet<Publisher> Publishers { get; set; }
6    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
7    {
8        optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=master;User=SA;Password=Password12!;");
9    }
10
11    protected override void OnModelCreating(ModelBuilder modelBuilder)
12    {
13        // Definição da relação com Author
14        modelBuilder.Entity<Book>()
15            .HasOne(b => b.Author)
16            .WithMany(a => a.Books)
17            .HasForeignKey(b => b.AuthorId);
18
19        // Definição da relação com Publisher
20        modelBuilder.Entity<Book>()
21            .HasOne(b => b.Publisher)
22            .WithMany(p => p.Books)
23            .HasForeignKey(b => b.PublisherId);
24    }
25}
```

[Acesse o código completo aqui](https://github.com/tryber/csharp-codes/blob/S5-D2-L3-EX2/ApiSql/Repository/BookContext.cs)

- Agora voltamos ao nosso `BookRepository`, que vai ser responsável por todos os registros necessários para criarmos um `Book`.
    
- Vamos passar uma instância de `BookContext` no nosso construtor e criar uma função chamada `Add` dentro dela.
    

Copiar

```csharp
1public class BookRepository
2{
3    protected readonly DbContext _context;
4    public BookRepository(DbContext context)
5    {
6        _context = context;
7    }
8
9    // ...
10
11    public Book Add(Book book)
12    {
13
14    }
15}
```

Agora vamos implementar nossa função `Add`:

Copiar

```csharp
1public Book Add(Book book)
2{
3    // Sempre utilizamos o contexto de banco de dados que 
4    // é um membro da classe BookRepository
5    _context.Add(book);
6    _context.SaveChanges();
7    return book;
8}
```

## Atualizando registros com repositórios

Por meio de repositórios conseguimos alterar dados de todos os membros desse relacionamento.

- Como as tabelas `Author` e `Publisher` possuem uma referência com a tabela `Book`, é possível alterarmos um dado delas utilizando essa referência.

![modelo de banco de dados](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-modelo%20de%20banco%20de%20dados.jpeg)

Vamos criar nosso repository `BookRepository` passando uma instância de `DbContext` no nosso construtor e declarar uma função chamada `Update` dentro dela.

Copiar

```csharp
1public class BookRepository
2{
3    protected readonly DbContext _context;
4    public BookRepository(DbContext context)
5    {
6        _context = context;
7    }
8
9    // ...
10
11    public Book Update(Book book)
12    {
13
14    }
15}
```

O método `Update` é simples, como recebemos o `Book` com os novos registros que queremos atualizar, basta chamar o método `Update` do contexto da seguinte forma:

Copiar

```csharp
1public class BookRepository
2{
3    protected readonly DbContext _context;
4    public BookRepository(DbContext context)
5    {
6        _context = context;
7    }
8
9    public virtual void Update(Book book)
10    {
11        _context.Update(book);
12        _context.SaveChanges();
13    }
14}
```

Vamos conferir como está nosso registro na tabela `Publisher` no banco de dados antes de rodarmos o programa:

![consulta no banco de dados](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-consulta%20no%20banco%20de%20dados.png)

Voltando ao banco de dados, realizaremos novamente a consulta na tabela `Publisher`.

![consulta no banco de dados depois do comando](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-consulta%20no%20banco%20de%20dados%20depois%20do%20comando.png)

Com o auxílio do repositório, conseguimos alterar vários dados em diferentes tabelas, em simultâneo, poupando tempo e código.🎉

## Não deixando pedra sobre pedra

Talvez você possa estar se perguntando: Se eu quiser apagar todos os registros relacionados a uma certa classe, eu consigo? 🤔 Resposta: Sim! Com repositórios também podemos fazer isso!

![botão delete](https://content-assets.betrybe.com/prod/34b23d36-6602-4c6c-9b70-aa3f78fcbc05-bot%C3%A3o%20delete.gif)

Para isso, criaremos um novo método chamado `DeleteBook` em nosso repositório `BookRepository` da seguinte forma:

Copiar

```csharp
1public class BookRepository
2{
3    protected readonly DbContext _context;
4    public BookRepository(DbContext context)
5    {
6        _context = context;
7    }
8    public virtual void DeleteBook(int id)
9    {
10        // realizamos uma busca em nossa base para encontrarmos
11        // o livro e suas associações, pelo id do livro, que 
12        // gostaríamos de excluir
13        var book = _context.Books.Include(e => e.Author).Include(e => e.Publisher).Single(p => p.BookId == id);
14        _context.Remove(book); // removemos o livro
15        _context.Remove(book.Author); //removemos o autor relacionado
16        _context.Remove(book.Publisher); // remove a editora associada
17        _context.SaveChanges(); //ao final salvamos nossa exclusão, para isso se refletir em nosso banco
18    }
19}
```

Ao chamarmos nossa função `Delete`, passamos um `id`, sendo a chave primária de `Books`. Dentro dessa função, vamos realizar uma pesquisa para encontrarmos nossa entidade mãe e suas tabelas agregadas. Por fim, removemos nossa instância de `Book` e as instâncias agregadas de `Author` e `Publisher`.

De olho na dica 👀: Esse comportamento de eliminar registros que não são mais necessários pode ser considerado uma boa prática de programação para não armazenarmos registros que não estão sendo usados, mantendo assim a integridade do nosso banco de dados.

[Acesse o código completo da repository aqui](https://github.com/tryber/csharp-codes/blob/S5-D2-L3-EX2/ApiSql/Repository/BookRepository.cs)

## Utilizando os repositórios

Para exemplificar o uso de nossos repositórios, vamos criar uma controller chamada `BookController`. Essa classe terá um construtor que receberá o nosso `BookRepository`. Isso só é possível graças à injeção de dependências.

Copiar

```csharp
1namespace ApiSql.Controllers;
2using Microsoft.AspNetCore.Mvc;
3using ApiSql.Models;
4using ApiSql.Repository;
5
6[ApiController]
7[Route("[controller]")]
8public class BookController : ControllerBase
9{
10
11    private readonly BookRepository _repository;
12    public BookController(BookRepository repository)
13    {
14        _repository = repository;
15    }
16}
```

Agora iremos criar um método `POST` fixo adicionando o livro `The Divine Comedy` para exemplificar:

Copiar

```csharp
1[HttpPost]
2public IActionResult AddBook()
3{
4    var book = new Book
5    {
6        Title = "The Divine Comedy",
7        Description = "A journey through the infinite torment of Hell",
8        Year = 2013,
9        Pages = 811,
10        Genre = "Drama",
11        Author = new Author
12        {
13            Name = "Dante Alighieri",
14            Email = "mail@mail.com"
15        },
16        Publisher = new Publisher
17        {
18            Name = "Paradise Publisher"
19        }
20    };
21
22    _repository.Add(book);
23
24    return Ok(new { message = "Book added"});
25}
```

[Acesse o código completo aqui](https://github.com/tryber/csharp-codes/blob/S5-D2-L3-EX2/ApiSql/Controllers/BookController.cs)

Note que instanciamos um novo `Book` e enquanto preenchemos os dados, instanciamos as classes `Author` e `Publisher` que irá gerar um novo livro completo. Ao chamar a `repository`, podemos ver no banco de dados que um novo `Book`, `Author` e `Publisher` são instanciados.