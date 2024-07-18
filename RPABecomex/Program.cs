// See https://aka.ms/new-console-template for more information

using RPABecomex;
using static RPABecomex.FindElement;

Browser browserInstance = new();

var browser = browserInstance.Open(
    "https://www3.fazenda.sp.gov.br/Simp/default.aspx",
    "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[2]/td[2]/input",
    30);

if (browser == null)
    return;

// Type CNPJ Importador
Type(browser,
    "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[2]/td[2]/input",
    "68912740000138");

// Type DI
Type(browser,
    "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[4]/td[2]/input",
    "2413142893");

// Click Gerar Documento
Click(browser,
    "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[6]/td/input[1]");

// Click GARE
Click(browser,
    "/html/body/form/div[4]/p/table/tbody/tr[8]/td/input[2]");

// Type Observações
Type(browser, 
    "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[10]/td/textarea", 
    "Observação teste");

// Type Data de Vencimento
Type(browser, 
    "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[11]/td[2]/input", 
    "123");

// Type Pagamento Até o Dia
Type(browser, 
    "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[12]/td[2]/input", 
    "123");

// Type Referência
Type(browser,
    "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[14]/td[2]/input", 
    "123");

//!!!!!!! TO DO !!!!!!!
//Select Recinto Alfandegado

// Type Valor Receita
Type(browser, 
    "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[20]/td[2]/input", 
    "123");

// Type Juros de Mora
Type(browser, 
    "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[21]/td[2]/input", 
    "123");

// Type Juros de Mora
Type(browser, 
    "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[22]/td[2]/input", 
    "123");


Console.WriteLine("Hello, World!");
