﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simulator
{
    public class FireAlert
    {
        public string Json;

        private string[] Niveis = new string[] { "Vermelho", "Laranja", "Amarelo", "Verde" };
        private string[] CodigoSAP = new string[] { "DA-L-32647", "DA-L-30814", "DA-L-39667", "DA-L-30719", "DA-L-30981", "DA-L-30357", "DA-L-30702", "T-L-30396", "T-L-30236", "T-L-39477", "DA-L-30723", "DA-L-30602", "T-L-30120", "T-L-30110", "DA-L-30820", "DA-L-30163", "T-L-30289", "T-L-30116", "T-L-30774", "T-L-30769", "DA-L-30441", "DA-L-30747", "DA-L-30689", "DA-L-34602", "DA-L-30622", "DA-L-30567", "DA-L-30678", "DA-L-30299", "DA-L-30714", "DA-L-36503", "DA-L-30777", "T-L-30207", "DA-L-30905", "DA-L-30790", "DA-L-30295", "DA-L-39634", "DA-L-38016", "DA-L-30626", "DA-L-39250", "DA-L-30491" };
        //TODO: Criar um objeto que vincule Regiões, Subestações e seus Municípios correspondentes.
        private string[] Regioes = new string[] { "METROPOLITANA", "MANTIQUEIRA", "OESTE", "CENTRO", "NORTE", "SUL", "LESTE", "TRIANGULO" };
        private string[] Municipios = new string[] { "Alfenas", "Araguari", "Araxá", "Barbacena", "Belo Horizonte", "Betim", "Brumadinho", "Confins", "Congonhas", "Conselheiro Lafaiete", "Contagem", "Divinópolis", "Extrema", "Governador Valadares", "Guaxupé", "Ibirité", "Ipatinga", "Itabira", "Itabirito", "Itajubá", "Itaúna", "Ituiutaba", "João Monlevade", "Juiz de Fora", "Lavras", "Manhuaçu", "Mariana", "Montes Claros", "Muriaé", "Nova Lima", "Ouro Branco", "Ouro Preto", "Pará de Minas", "Paracatu", "Passos", "Patos de Minas", "Patrocínio", "Pedro Leopoldo", "Pirapora", "Poços de Caldas", "Pouso Alegre", "Ribeirão das Neves", "Sabará", "Santa Luzia", "São Gonçalo do Rio Abaixo", "São Sebastião do Paraíso", "Sete Lagoas", "Teófilo Otoni", "Timóteo", "Tiros", "Três Corações", "Ubá", "Uberaba", "Uberlândia", "Unaí", "Varginha", "Vespasiano" };
        private string[] Subestacoes = new string[] { "ABADIA DOS DOURADOS 2", "ABAETE 2", "ACUCAREIRA ITAIQUARA", "ADM (CONSUMIDOR - UBERLANDIA)", "AIMORES", "AIR LIQUIDE", "ALCOA", "ALFENAS 1", "ALFENAS 2", "ALMENARA 1", "ALPARGATAS", "ALPINOPOLIS 2", "AMBEV (CONSUMIDOR)", "ANDRADAS 2", "ANGLO GOLD/QUEIROZ", "ARACAGI", "ARACUAI 1", "ARACUAI 2", "ARAGUARI 2", "ARAPORA", "ARAXA 1", "ARAXA 2", "ARCOS", "ARCOS 2", "ARINOS", "ATALAIA", "AVATINGUARA", "BAMBUI", "BARAO DE COCAIS 1", "BARAO DE COCAIS 4", "BARBACENA 2", "BARREIRO 1", "BARREIRO 3", "BARROSO 3", "BARROSO 4", "BELGO MIN / BEKAERT - VESPASIANO", "BELGO MIN. BEKAERT - ITAUNA", "BELGO MIN./CONTAGEM", "BELGO MIN./SABARÃ", "BELOCAL  SÃƒO JOSÃ‰ DA LAPA (ITAU)", "BELOCAL MATOZINHOS", "BERILO", "BETIM 2", "BETIM 3", "BETIM 4", "BETIM 5", "BH ADELAIDE", "BH SAO MARCOS", "BH-HORTO", "BH-SERRA VERDE", "BOA ESPERANCA 2", "BOCAIUVA", "BOM DESPACHO 2", "BOM SUCESSO", "BONFINOPOLIS DE MINAS", "BONSUCESSO", "BORDA DA MATA", "BOTELHOS", "BRAGANTINA", "BRASILANDIA 2", "BRASILIA DE MINAS", "BRASOPOLIS 1", "BRECHA", "BRUMADINHO", "BURITIS 1", "BURITIS 2", "BURITIZEIRO", "CACHOEIRA DOURADA", "CAETE", "CAMBUQUIRA 1", "CAMPANARIO", "CAMPINA VERDE", "CAMPO BELO", "CAMPO DO MEIO", "CAMPOS ALTOS", "CAMPOS GERAIS", "CAPELINHA 1", "CAPINOPOLIS", "CARANDAI 2", "CARANDAI 3", "CARANGOLA", "CARATINGA 1", "CARGILL", "CARLOS CHAGAS 1", "CARLOS CHAGAS 2", "CARMO DO CAJURU", "CARMO DO PARANAIBA 2", "CARMO DO RIO CLARO", "CARMOPOLIS DE MINAS", "CARNEIRINHO", "CASSIA 1", "CAXAMBU", "CEDRO CACHOEIRA", "CENTRAL DE MINAS", "CENTRALINA", "CI SANTA LUZIA", "CIDADE INDUSTRIAL", "CINCO", "CLAUDIO 1", "CLAUDIO 2", "CMM 2 - VAZANTE", "CNC / COMPANHIA NACIONAL DE CIMENTO", "COMENDADOR GOMES", "COMINCI", "CONCEICAO ALAGOAS", "CONCEICAO APARECIDA", "CONCEICAO DO MATO DENTRO", "CONGONHAS 1", "CONSELHEIRO LAFAIETE 1", "CONSELHEIRO PENA", "CONTAGEM 3", "CONTAGEM 5", "COPASA BETIM", "COPASA BRUMADINHO", "COPASA/CONTAGEM", "COPASA/INHOTIM", "CORACAO DE JESUS", "CORDISBURGO", "CORINTO 1", "COROACI 1", "COROMANDEL", "CORONEL FABRICIANO", "COTENOR", "COUTO MAGALHAES", "CSN - Congonhas", "CURIMBABA (EX. MITSUI)", "CURVELO 1", "CURVELO 2", "DAIMLER CHRYSLER ( MERCEDES BENS )", "DAIWA", "DEMETRÃ” F", "DEMETRO A", "DIAMANTINA 1", "DORES DO INDAIA", "DVINOPOLIS 1", "DVINOPOLIS 2", "EMSA", "ENGENHEIRO CALDAS", "ENGENHEIRO DOLABELA", "ESMAN CHAVEAMENTO CEMIG", "ESMERALDAS", "ESPINOSA", "FELIXLANDIA", "FORMIGA", "FRANCISCO SA", "FRANCISCO SA 2", "FREI INOCENCIO", "FRUTAL 1", "FRUTAL 2", "FURNAS - POCOS DE CALDAS", "GERDAU - AÃ§ominas", "GERDAU BARÃƒO DE COCAIS", "GOUVEIA 2", "GOUVEIA 3", "GOVERNADOR VALADARES 1", "GOVERNADOR VALADARES 2", "GOVERNADOR VALADARES 3", "GOVERNADOR VALADARES 4", "GUANHAES", "GUANHAES 2", "GUARDA MOR", "GUAXUPÃ‰ 2", "GUTIERREZ", "HOLDERCIM / PEDRO LEOPOLDO", "IBIA 2", "IBIRITÃ‰ 2", "ICAL SÃƒO JOSÃ‰ DA LAPA", "ICAL-ARCOS", "ICARAI DE MINAS", "IGARAPE 1", "IGARAPE 2", "IGUATAMA 2", "ILICINEA", "INB", "INHAPIM 2", "INIMUTABA", "INONIBRAS", "INTERCAST", "INTERCEMENT (CAMARGO CORRÃŠA)", "IPATINGA 1", "IPATINGA 2", "IPATINGA 3", "IRAI DE MINAS", "ITABIRA 2", "ITABIRA 3", "ITABIRITO 1", "ITABIRITO 3", "ITACARAMBI 2", "ITAJUBA 3", "ITAJUBÃ 1", "ITALMAGNÃ‰SIO", "ITAMARANDIBA", "ITANHANDU", "ITANHANDU 2", "ITAOBIM", "ITAPAGIPE", "ITAPECERICA", "ITATIAIUÃ‡U", "ITAU DE MINAS", "ITAU(ARCOS-LIMEIR)", "ITAUNA 1", "ITAUNA 2", "ITAUNA SIDERURGICA", "ITUIUTABA 1", "ITURAMA 1", "IVECO FIAT", "JABOTICATUBAS", "JANAUBA 1", "JANAUBA 2", "JANUARIA 2", "JANUARIA 3", "JATOBA", "JEQUITAI", "JEQUITINHONHA", "JOAO MONLEVADE 3", "JOAO PINHEIRO 2", "JOAO PINHEIRO 3", "JOAO PINHEIRO I", "JOAQUIM MURTINHO", "JORDANIA", "JUIZ DE FORA 1", "JUIZ DE FORA 7", "JUIZ DE FORA 8", "KRUPP", "LAFARGE/MATOZINHOS", "LAGOA FORMOSA", "LAGOA GRANDE", "LAGOA SANTA", "LAJINHA", "LAMBARI", "LAVRAS 2", "LEANDRO FERREIRA", "LIASA", "LONTRA", "LUZ 1", "MACHADO 2", "MACHADO MINEIRO", "MAGNESITA", "MAGNESITA/ELETROFUSAO", "MALACACHETA", "MANGA 1", "MANGA 3", "MANGA 5", "MANHUACU", "MANTENA", "MARACANA", "MARIANA 1", "MARIANA 2", "MARTINHO CAMPOS", "MATO VERDE", "MATOZINHOS", "MIGUEL BURNIER", "MINAS LIGAS", "MINAS NOVAS 1", "MINDURI", "MINER. MORRO AZUL", "MIRABELA", "MONJOLOS 1", "MONTE ALEGRE DE MINAS", "MONTE AZUL", "MONTE SIAO", "MONTES CLAROS 1", "MONTES CLAROS 2", "MONTES CLAROS 4", "MORADA NOVA DE MINAS", "MORRO DO CHAPEU", "MORRO GARRAFAO", "MUZAMBINHO 2", "NEMAK", "NEPOMUCENO", "NEVES 1", "NEVES II", "NEVES III", "NOVA GRANJA", "NOVA LIMA 1", "NOVA LIMA 4", "NOVA LIMA 5", "NOVA PONTE", "NOVA PONTE 2", "NOVA RESENDE", "NOVA SERRANA", "NOVELLIS", "NOVO CRUZEIRO", "NOVO NORDISK/MONTES CLAROS", "OLIVEIRA", "OURO PRETO 1", "OURO PRETO 2", "OURO PRETO 3", "PADRE PARAISO", "PAI JOAQUIM", "PAINEIRAS", "PAINS 2", "PAMPULHA", "PAPAGAIOS", "PARA DE MINAS 1", "PARA DE MINAS II", "PARACATU 1", "PARACATU 3", "PARACATU 4", "PARACATU 5", "PARACATU II", "PARAGUAÃ‡U 1", "PARAISÃ“POLIS", "PARAOPEBA", "PARAUNA", "PASSOS 1", "PATOS DE MINAS 1", "PATOS DE MINAS 2", "PATROCINIO 1", "PECANHA 2", "PEDRA AZUL", "PEDRA DO INDAIA", "PEDRINOPOLIS", "PEDRO LEOPOLDO 3", "PERDIZES", "PERDOES", "PETROBRÃS - UBERABA", "PIMENTA", "PIPOCA", "PIRAJUBA", "PIRAPORA 1", "PIRAPORA 2", "PITANGUI 2", "PIUMHI 2", "POCOS DE CALDAS 1", "PONTE NOVA", "PORTEIRINHA 2", "PORTO COLOMBIA", "PORTO FIRME", "POTE", "POUSO ALEGRE", "POUSO ALEGRE 2", "PRATA 1", "PRATA 2", "PRATAPOLIS", "PRESIDENTE BERNARDES", "RAUL SOARES", "RESPLENDOR", "RIACHINHO", "RIMA - BOCAIUVA", "RIO ACIMA 1", "RIO ACIMA 3", "RIO CASCA", "RIO DAS PEDRAS", "RIO DO PRADO", "RIO ESPERA", "RIO PARANAIBA", "RISOLETA NEVES", "SABARA", "SABINOPOLIS", "SACRAMENTO", "SADIA RAÃ‡Ã•ES", "SÃƒO SEBASTIÃƒO DO OESTE", "SAINT GOBAIN", "SALINAS", "SAMARCO BOOSTER 2", "SANTA BARBARA 1", "SANTA EFIGENIA", "SANTA LUZIA 2", "SANTA LUZIA 4", "SANTA LUZIA I", "SANTA MARIA DO SUACUI", "SANTA QUITERIA", "SANTA RITA DE CALDAS", "SANTA VITORIA", "SANTANA DA VARGEM", "SANTO ANTONIO AMPARO", "SANTOS DUMONT 1", "SANTOS DUMONT 2", "SAO FRANCISCO 4", "SAO FRANCISCO DE PAULA", "SAO GONCALO DO ABAETE I", "SAO GONCALO DO PARA", "SAO GONCALO DO SAPUCAI", "SAO GOTARDO 1", "SAO JOAO DEL REI 1", "SAO JOAO DEL REI II", "SAO JOAO EVANGELISTA", "SAO LOURENÃ‡O 1", "SAO PEDRO DO SUACUI", "SAO SEBASTIAO PARAISO", "SE OURO FINO", "SE USINA EMBORCACAO", "SERRO", "SETE LAGOAS 3", "SETE LAGOAS 4", "SETE LAGOAS I", "SETE LAGOAS II", "SHANTER", "SION", "SOBRAGI", "SOUZA CRUZ", "STEPAN QUÃMICA LTDA", "TAIOBEIRAS", "TAQUARIL", "TEKFOR BETIM", "TEKSID", "TEOFILO OTONI 1", "TRÃŠS CORAÃ‡OES 2", "TRÃŠS PONTAS 1", "TRES CORACOES 1", "TUPACIGUARA 2", "UBERABA 1", "UBERABA 3", "UBERABA 4", "UBERABA 5", "UBERABA 9", "UBERLÃ‚NDIA 9", "UBERLANDIA 1", "UBERLANDIA 2", "UBERLANDIA 6", "UNAI 2", "UNAI 3", "UNAI 5", "UNIFI", "USIMEC", "USINA DE ITUTINGA", "USINA DO PIAU", "USINA EOLICA DE CAMELINHO", "USINA GAFANHOTO", "USINA HIDROELETRICA DE JAGUARA", "USINA MIRANDA", "USINA PANDEIROS", "USINA PETI", "USINA QUEIMADOS", "USINA TRES MARIAS", "V&M DO BRASIL", "VALE - Timbopeba", "VALE ALEGRIA", "VALE FERTILIZANTES ARAXÃ", "VALE FERTILIZANTES-UBERABA", "VALE/MINA FABRICA", "VALE/NOVA LIMA", "VARGINHA 1", "VARGINHA 2", "VARZEA PALMA", "VDL", "VESPASIANO (CHAVEAMENTO)", "VESPASIANO 2", "VOLTA GRANDE", "VOTORANTIM CIMENTOS", "VOTORANTIM METAIS ZINCO", "W.MARTINS - DIVINOPOLIS", "WHITE MARTINS/BH" };
        private string[] Linhas = new string[] { "BARAO DE COCAIS 2 - SAO BENTO MINERACAO 230 kV", "BARBACENA 2 - LAFAIETE 1", "BARBACENA 2 - PIMENTA", "BARREIRO - NEVES 1", "BARREIRO - TAQUARIL", "BOM DESPACHO 3 - SAO GONCALO DO PARA", "BOM DESPACHO 3 - SAO GOTARDO 2", "EMBORCACAO - NOVA PONTE", "EMBORCACAO - SAO GOTARDO 2", "GUILMAN AMORIM - IPATINGA 1", "IPATINGA 1 - PORTO ESTRELA", "JAGUARA - NOVA PONTE", "JAGUARA - SAO SIMAO", "JAGUARA - VOLTA GRANDE", "LT BAGUARI - GOVERNADOR VALADRES 2 - 230 KV", "LT BAGUARI - MESQUITA - 230 KV", "LT BARAO DE COCAIS 3 - TAQUARIL", "LT BARAO DE COCAIS 3-JOAO MONLEVADE 2-230KV", "LT BARBACENA 2 - SANTOS DUMONT 2 - 345 KV", "LT ITABIRA 2 - SABARA 3 230 KV", "LT ITABIRA 4 - TAQUARIL - 230 KV", "LT JECEABA â€“ ITABIRITO 2", "LT JECEABA-LAFAIETE - 345 KV.", "LT NOVA LIMA 6 - OURO PRETO 2 - 345 KV", "LT NOVA LIMA 6 - TAQUARIL - 345 KV", "LT OURO PRETO 2 â€“ ITABIRITO 2", "LT SABARA 3 -TAQUARIL 230 KV", "LT SANTOS DUMONT 2 - JUIZ FORA 1 - 345 KV", "LT TRÃŠS MARIAS â€“ SETE LAGOAS 4", "LT1 BOM DESPACHO 3 - JAGUARA 500", "LT1 BOM DESPACHO 3 - NEVES 1", "LT1 JAGUARA - PIMENTA", "LT2 BOM DESPACHO 3 - JAGUARA 500", "LT2 BOM DESPACHO 3 - NEVES 1", "LT2 GOVERNADOR VADARES 2 - MESQUITA", "LT2 JAGUARA - PIMENTA", "LT2 PIRAPORA 2 - VARZEA DA PALMA 1", "MESQUITA - VESPASIANO 2", "MONTES CLAROS 2 - VARZEA DA PALMA 1", "NEVES 1 - TAQUARIL", "NEVES 1 - VESPASIANO 2", "PIMENTA - TAQUARIL", "SAO GOTARDO 2 - TRES MARIAS", "TRES MARIAS - VARZEA DA PALMA 1" };
        private string[] Estruturas = new string[] { "016 >> 015", "121 >> 120", "018A >> PLAFA", "088 >> 089", "087 >> 088", "086 >> 087", "008 >> 009", "64 >> 65", "014B >> PTIP3", "028 >> PT-US", "222 >> 221", "147 >> 146", "09 >> 08", "PCNC >> 001", "PTHOL >> 09", "012 >> 013", "168 >> 169", "270 >> 271", "271 >> 272", "136 >> 137", "033 >> 034", "037 >> 038", "032 >> 033", "034 >> 035", "039 >> 040", "040 >> 041", "041 >> 042", "043 >> 044", "045 >> 046", "367 >> 368", "344 >> 345", "345 >> 346", "342 >> 343", "343 >> 344", "335 >> 336", "346 >> 347", "337 >> 338", "336 >> 337", "340 >> 341", "341 >> 342", "338 >> 339", "334 >> 335", "339 >> 340", "540 >> 541", "POBD3 >> 536C", "536C >> 536B", "059 >> 060", "061 >> PTACO", "040 >> 039", "40 >> PTGER", "073 >> 074", "05 >> PTCTU", "070 >> 069", "071 >> 070", "018 >> 019", "151 >> 152", "153 >> 154", "152 >> 153", "096D >> PGRLI", "361 >> 362", "068 >> 069", "060 >> 059", "061 >> 060", "055 >> 054", "056 >> 055", "014 >> 015", "PTCIT >> 06A", "066 >> 065", "011 >> 012", "199 >> 198", "22 >> 23" };
        private string[] Natureza = new string[] { "Madeira", "Metálico (T)", "Metálico (D)", "Concreto" };
        private string[] ReligamentoEspecial = new string[] { "S", "N" };
        private string[] NivelTensao = new string[] { "34,5 KV", "69 KV", "138 KV", "230 KV", "345 KV", "500 KV" };
        private List<double[]> Coordenadas = new List<double[]> { new double[] { -41.923828125, -16.13026201203474 }, new double[] { -42.82470703125, -15.940202412387029 }, new double[] { -45.37353515625, -15.47485740268724 }, new double[] { -46.12060546875, -17.936928637549432 }, new double[] { -44.6923828125, -19.2489223284628 }, new double[] { -42.86865234375, -19.91138351415555 }, new double[] { -42.82470703125, -18.18760655249461 }, new double[] { -44.47265625, -20.899871347076424 }, new double[] { -45.68115234375, -19.91138351415555 }, new double[] { -46.95556640624999, -19.228176737766248 }, new double[] { -44.033203125, -17.09879223767869 }, new double[] { -43.87939453125, -15.623036831528252 }, new double[] { -45.46142578125, -16.55196172197251 }, new double[] { -41.37451171875, -17.476432197195518 }, new double[] { -41.77001953125, -19.62189218031936 }, new double[] { -42.47314453125, -20.427012814257385 }, new double[] { -43.00048828125, -21.02298254642741 }, new double[] { -43.9013671875, -21.53484700204879 }, new double[] { -45, -21.739091217718574 }, new double[] { -45.68115234375, -20.488773287109822 }, new double[] { -46.142578125, -21.002471054356715 }, new double[] { -46.669921875, -19.76670355171696 }, new double[] { -48.05419921875, -19.68397023588844 }, new double[] { -49.59228515625, -19.476950206488414 }, new double[] { -44.29687499999999, -18.1249706393865 }, new double[] { -46.58203125, -16.34122561920748 } };

        public FireAlert()
        {
            var json = ReadAsJson("Model/FireAlert_v1.json");

            //json["value_schema"] = FireAlert_v1._SCHEMA.ToString();
            json["value_schema"] = json["value_schema"].ToString();

            var coordenada = Coordenadas.OrderBy(x => new Random().Next()).Take(1).ToArray();

            var value = (JObject)json["records"][0]["value"];

            value["Id"] = DateTime.Now.Ticks.ToString();
            value["Nivel"] = Niveis[new Random().Next(Niveis.Length - 1)];
            value["DataHora"] = DateTime.Now.ToUniversalTime().ToString("o");
            value["Regioes"] = string.Join(',', Regioes.OrderBy(x => new Random().Next()).Take(new Random().Next(Regioes.Length - 1)).ToArray());
            value["Municipios"] = string.Join(',', Municipios.OrderBy(x => new Random().Next()).Take(new Random().Next(Municipios.Length - 1)).ToArray());
            value["Subestacoes"] = string.Join(',', Subestacoes.OrderBy(x => new Random().Next()).Take(new Random().Next(Subestacoes.Length - 1)).ToArray());
            value["Linhas"] = string.Join(',', Linhas.OrderBy(x => new Random().Next()).Take(new Random().Next(Linhas.Length - 1)).ToArray());
            value["Estrutura"] = Estruturas.OrderBy(x => new Random().Next()).ToArray()[0].ToString();
            value["Natureza"] = Natureza.OrderBy(x => new Random().Next()).ToArray()[0].ToString();
            value["ReligamentoEspecial"] = ReligamentoEspecial.OrderBy(x => new Random().Next()).ToArray()[0].ToString();
            value["NivelTensao"] = NivelTensao.OrderBy(x => new Random().Next()).ToArray()[0].ToString();
            value["Longitude"] = coordenada[0][0].ToString();
            value["Latitude"] = coordenada[0][1].ToString();

            Json = JsonConvert.SerializeObject(json);
        }

        private JObject ReadAsJson(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                return JObject.Parse(json);
            }
        }
    }
}