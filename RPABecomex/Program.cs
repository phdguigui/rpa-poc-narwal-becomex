// See https://aka.ms/new-console-template for more information

using OpenQA.Selenium;
using RPABecomex;
using System.Diagnostics;
using static RPABecomex.FindElement;

Browser browserInstance = new();
bool isGareProcess = true;
bool isGuiaLiberacaoProcess = false;
string mensagemRetorno = "";

if (isGareProcess && !isGuiaLiberacaoProcess)
{
    GareProcess();
}
else if (!isGareProcess && isGuiaLiberacaoProcess)
{
    GuiaLiberacaoProcess();
}
else
{
    mensagemRetorno = "Erro ao iniciar processo RPA.";
}

void GareProcess ()
{
    var browser = browserInstance.Open(
        "https://www3.fazenda.sp.gov.br/Simp/default.aspx",
        "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[2]/td[2]/input",
        30);

    if (browser == null)
    {
        mensagemRetorno = "Portal da Fazenda indisponível.";
        return;
    }

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

    if (GetElement(browser, "/html/body/form/div[4]/p/table/tbody/tr[8]/td/input", 10) == null)
    {
        mensagemRetorno = "Erro ao encontrar dados com o CNPJ e DI informados. " +
            GetElement(browser, "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/span", 5)?.Text;
        return;
    }

    // Click GARE
    if (!ClickOptionButton(browser,
        "/html/body/form/div[4]/p/table/tbody/tr[8]/td/input", "GARE"))
    {
        mensagemRetorno = "Erro ao encontrar botão para geração da GARE.";
        return;
    }

    if (GetElement(browser, "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[10]/td/textarea", 5) == null)
    {
        mensagemRetorno = "Falha ao carregar página de informações da GARE.";
        return;
    }

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

    //Select Recinto Alfandegado
    if (!SelectOption(browser, "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[16]/td[2]/select", "8921101"))
    {
        mensagemRetorno = "Falha ao encontrar opção de recinto alfandegário.";
        return;
    }

    // Type Valor Receita
    Type(browser, 
        "/html/body/form/div[4]/p/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr[20]/td[2]/input", 
        "123");

    // Espera da geração da GARE
    bool genDoc = false;

    Stopwatch sw = new();
    sw.Start();
    while (sw.Elapsed.TotalSeconds < 300)
    {
        genDoc = GetElement(browser, "/html/body/form/table/tbody/tr/td[3]/input[1]", 2) != null;
        if (genDoc)
        {
            mensagemRetorno = "Sucesso ao gerar GARE.";
            break;
        }
    }

    if (!genDoc)
    {
        mensagemRetorno = "Falha ao gerar GARE.";
        return;
    }
}

void GuiaLiberacaoProcess()
{
    var browser = browserInstance.Open(
    "https://www3.fazenda.sp.gov.br/Simp/default.aspx",
    "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[2]/td[2]/input",
    30);

    if (browser == null)
    {
        mensagemRetorno = "Portal da Fazenda indisponível.";
        return;
    }

    // Type CNPJ Importador
    Type(browser,
        "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[2]/td[2]/input",
        "01576749000225");

    // Type DI
    Type(browser,
        "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[4]/td[2]/input",
        "2414699743");

    // Click Gerar Documento
    Click(browser,
        "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/table/tbody/tr[6]/td/input[1]");

    // Edição das Adições
    if (GetElement(browser, "/html/body/form/div[4]/p/table/tbody/tr[8]/td/input", 10) == null)
    {
        mensagemRetorno = "Erro ao encontrar dados com o CNPJ e DI informados. " +
            GetElement(browser, "/html/body/form/div[4]/p[1]/table/tbody/tr[1]/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]/span", 5)?.Text;
        return;
    }
    var adicaoList = browser.FindElements(By.XPath($"/html/body/form/div[4]/p/table/tbody/tr[5]/td/table/tbody/tr[2]/td[1]/input"));

    foreach (var adicao in adicaoList)
    {
        // Click Editar
        adicao.Click();
        // Type Fundamento Legal da Exoneração
        Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[3]/td/table[2]/tbody/tr[4]/td/textarea", "Observação teste");
        // Click Calcular
        Click(browser, "/html/body/form/div[4]/p/table/tbody/tr[6]/td/input[2]");
        // Click Gravar
        Click(browser, "/html/body/form/div[4]/p/table/tbody/tr[6]/td/input[3]");
        // Click Voltar
        Click(browser, "/html/body/form/div[4]/p/table/tbody/tr[6]/td/input[1]");
    }

    // Click Guia de Liberação
    if(!ClickOptionButton(browser, "/html/body/form/div[4]/p/table/tbody/tr[8]/td/input", "Guia de Liberação"))
    {
        mensagemRetorno = "Botão de Guia de Liberação não encontrado.";
        return;
    }

    if (GetElement(browser, "/html/body/form/div[4]/p/table/tbody/tr[3]/td/table/tbody/tr/td/table/tbody/tr[3]/td[1]/input", 5) == null)
    {
        mensagemRetorno = "Falha ao carregar página de informações da Guia de Liberação.";
        return;
    }

    // Type Valor CIF
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[3]/td/table/tbody/tr/td/table/tbody/tr[3]/td[1]/input", "123");

    // Select UF Desembaraço
    if (!SelectOption(browser, "/html/body/form/div[4]/p/table/tbody/tr[3]/td/table/tbody/tr/td/table/tbody/tr[3]/td[2]/select", "SP"))
    {
        mensagemRetorno = "Falha ao encontrar UF Desembaraço infomada.";
        return;
    }

    // Type Data Declaração Siscomex
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[3]/td/table/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr/td[1]/input", "01/01/2024");

    // Select UF Desembarque
    if (!SelectOption(browser, "/html/body/form/div[4]/p/table/tbody/tr[3]/td/table/tbody/tr/td/table/tbody/tr[4]/td/table/tbody/tr/td[2]/select", "SP"))
    {
        mensagemRetorno = "Falha ao encontrar UF Desembarque informada.";
        return;
    }

    // Select Local Desembaraço
    if (!SelectOption(browser, "/html/body/form/div[4]/p/table/tbody/tr[3]/td/table/tbody/tr/td/table/tbody/tr[5]/td/select", "8921101"))
    {
        mensagemRetorno = "Falha ao encontrar Local Desembaraço informado.";
        return;
    }

    // Type Nome
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[2]/td/input", "Guilherme Lucas");

    // Type CPF
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[3]/td/input", "00000000000");

    // Type Endereço
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[4]/td/input", "Rua Teste");

    // Type Bairro/Distrito
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[5]/td[1]/input", "Bairro Teste");

    // Type Município
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[5]/td[2]/input", "Município Teste");

    // Type CEP
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[6]/td[1]/input", "00000000");

    // Select UF
    if (!SelectOption(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[6]/td[1]/select", "SP"))
    {
        mensagemRetorno = "Falha ao encontrar UF informada.";
        return;
    }

    // Type DDD
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[6]/td[2]/input[1]", "11");

    // Type Telefone
    Type(browser, "/html/body/form/div[4]/p/table/tbody/tr[4]/td/table/tbody/tr/td/table/tbody/tr[6]/td[2]/input[2]", "11111111");


    // Espera da geração da Guia de Liberação
    bool genDoc = false;

    Stopwatch sw = new();
    sw.Start();
    while (sw.Elapsed.TotalSeconds < 300)
    {
        genDoc = GetElement(browser, "/html/body/form/table/tbody/tr/td[3]/input[1]", 2) != null;
        if (genDoc)
        {
            mensagemRetorno = "Sucesso ao gerar Guia de Liberação.";
            break;
        }
    }

    if (!genDoc)
    {
        mensagemRetorno = "Falha ao gerar Guia de Liberação.";
        return;
    }
}
