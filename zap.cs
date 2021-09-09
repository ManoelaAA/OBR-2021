void Main(){

string Estado = "nulo";

double SensorDistanciaFrenteCimaSemDiferença = 0;

bool DistanciaVitimaMenor25 = false;

double R = 0;
double B = 0;
double G = 0;

double H = 0;

// Determina um nome pra cada sensor utilizado
int IDSensorCorPontaDireita = 0;
int IDSensorCorDireita = 1;
int IDSensorCorMeio = 2;
int IDSensorCorEsquerda = 3;
int IDSensorCorPontaEsquerda = 4;
int IDSensorCorFrente = 5;

float DireçãoDoRobô = 0;

bool Maior180 = false;
float AnguloZero = 0;
float AnguloFinal = 0;

float Vermelho = 0;
float Verde = 0;
float Azul = 0;

float RGBSensorCorMeio = 0;

//Determina os dados padrões das velocidades, distancias e inclinações
int VelocidadeVitimaMenor25Frente = 100;
int VelocidadeVitimaMenor25Tras = -100;

int DistanciaObstaculo = 5;

int AnguloDobraAreaSalvamento = 45;

int VelocidadeCurva = 500;
int VelocidadeAjuste = 200; // Do Whi leAlinhamento
int VelocidadeMaximaFrente = 300;
int VelocidadeMaximaTras = -300;

int VelocidadeBranco = 120;

int PretoPontaEsquerda = 0;
int PretoPontaDireita = 0;
int PretoDireita = 0;
int PretoEsquerda = 0;

int DistanciaAlinhamentoVerde = 15;

int DistanciaAlinhaArea = 80;

float QuantidadeGiro = 0;
int SentidoCurva = 0;

float GiroIdentificaçãoVitima = 0;

int VelocidadeObstaculo = 135;



// Retorna determinada angulação (cima e baixo) vista pelo sensor estabelecido no comando
Func<float> InclinaçãoHorizontal = () => bc.inclination();

// Retorna determinada angulação (esquerda e direita) vista pelo sensor estabelecido no comando
Func<float> Direção = () => bc.compass(); 

//Retorna o tempo em milissegundos decorrido desde o início da execução da rotina ou desde o último comando de zerar o temporizador; 
Func<int> Temporizador = () => bc.timer();
//Zera o tempo decorrido no temporizador e inicia uma nova contagem


Action ZerarTemporizador = () => {
    bc.resetTimer();
};

// Escreve na tela o que esta definido entre os parenteses, onde o primeiro espaço é a linha que sera escrito no console e o segundo espaço é o texto em si
Action<int, string> EscreverNaTela = (linha, texto) => {
    bc.printLCD(linha, texto);
};

// Comando de andar, cada espaço nos parenteses é a força do motor
Action<int, int> Andar = (forçaMotorDireito, forçaMotorEsquerdo) => {
    bc.MoveFrontal(forçaMotorDireito, forçaMotorEsquerdo);
};

// Comando de rotacionar por graus, onde o primeiro espaço nos parenteses é a força dos motores e a segunda é o grau que o robô ira rotacionar NO PROPRIO EIXO 
Action<int, float> Rotacionar = (forçaMotor, angulo) => {
    bc.MoveFrontalAngles(forçaMotor, angulo);
};

Action<float, float> AndarPorRotaçoes = (forçaMotores, rotaçoes) => {
    bc.MoveFrontalRotations(forçaMotores, rotaçoes);
};

// Liga o led na cor estabelecida nos parenteses
Action<string> LigarLed = (cor) => {
    bc.TurnLedOn(cor);
};

// Tempo que será realizado a ação (após andar) ou tempo de espera(após rotacionar)
Action<int> Esperar = (tempo) => {
    bc.wait (tempo);
};

// Move o atuador pra cima, o numero entre os parenteses é o tempo de duração da ação
Action<int> AtuadorCima = (atuadorCima) => {
  bc.actuatorUp(atuadorCima);
};

// Move o atuador pra baixo, o numero entre os parenteses é o tempo de duração da ação
Action<int> AtuadorBaixo = (atuadorBaixo) => {
   bc.actuatorDown(atuadorBaixo);
};

// Move o atuador, em graus, pra cima, o numero entre os parenteses é o tempo de duração da ação
Action<int> GirarAtuadorCima = (tempoAtuadorCima) => {
  bc.turnActuatorUp(tempoAtuadorCima);
};

// Move o atuador, em graus, pra baixo, o numero entre os parenteses é o tempo de duração da ação
Action<int> GirarAtuadorBaixo = (tempoAtuadorBaixo) => {
   bc.turnActuatorDown(tempoAtuadorBaixo);
};

// Para ambos os motores do robô
Action Parar = () => {
    bc.MoveFrontal(0, 0);
};

string DecToHex(float sensor){
    
    string hexStr1 = Convert.ToString((int)bc.returnRed((int)sensor), 16);
    string hexStr2 = Convert.ToString((int)bc.returnGreen((int)sensor), 16);
    string hexStr3 = Convert.ToString((int)bc.returnBlue((int)sensor), 16);

    if (hexStr1.Length<2){
        hexStr1 = "0" + hexStr1;
    };
    if (hexStr2.Length<2){
        hexStr2 = "0" + hexStr2;
    };
    if (hexStr3.Length<2){
        hexStr3 = "0" + hexStr3;
    };
    return $"{hexStr1}{hexStr2}{hexStr3}";
};




//Atualiza o todas as variaveis com os retornos dos sensors quando chamada no código
Action AtualizarVariáveis = () => {
    
    //SensorCorPontaEsquerda = PegaCor(IDSensorCorPontaEsquerda);
    //SensorCorEsquerda = PegaCor(IDSensorCorEsquerda);
    //SensorCorMeio = PegaCor(IDSensorCorMeio);
    // SensorCorDireita = PegaCor(IDSensorCorDireita);
    // SensorCorPontaDireita = PegaCor(IDSensorCorPontaDireita);
    //SensorCorFrente = PegaCor(IDSensorCorFrente);

    // SensorDistanciaFrenteBaixo = PegaDistancia(IDSensorDistanciaFrenteBaixo);
    // SensorDistanciaFrenteCima = PegaDistancia(IDSensorDistanciaFrenteCima);
    // SensorDistanciaDireita = PegaDistancia(IDSensorDistanciarDireita);

    // EstadoSensortoque = PegaEstadoToque(IDSensorToque);

    //SensorDistanciaFrenteCimaSemDiferença = SensorDistanciaFrenteCima - 6;
    
    // InclinaçãoDoRobô = bc.inclination();
    DireçãoDoRobô = Direção();
    
    Vermelho = bc.returnRed(IDSensorCorFrente);
    Verde = bc.returnGreen(IDSensorCorFrente);
    Azul = bc.returnBlue(IDSensorCorFrente);
    RGBSensorCorMeio = bc.returnRed(IDSensorCorMeio);

    H = 0;
    
};


Action<int> VelocidadeAtuador = (velocidadeatuador) => {
    bc.ActuatorSpeed(velocidadeatuador);
};


Action <int, float> WhileAlinhamento = (AnguloDestino, Sentido) => {  
    var cont = 0;
    //Alinhamento (de acordo com a angulação) do robo
    while(Convert.ToInt32(bc.Compass()) > AnguloDestino + 1 || Convert.ToInt32(bc.Compass()) < AnguloDestino - 1){
        EscreverNaTela(4, cont.ToString());
        if(cont > 20 && Sentido < 0 && Convert.ToInt32(bc.Compass()) < AnguloDestino - 3 && bc.Compass() < AnguloDestino - 0.5){
            Rotacionar(VelocidadeAjuste, -Sentido*500);
        }
        else if(cont > 20 && Sentido > 0 && Convert.ToInt32(bc.Compass()) > AnguloDestino + 3){
            Rotacionar(VelocidadeAjuste, -Sentido*500);
        }
        Rotacionar(VelocidadeAjuste, Sentido);
        EscreverNaTela(3, "Fazendo: " + Estado + " Ângulo: " + bc.Compass().ToString());
        LigarLed("AZUL");

        Parar();
        AtualizarVariáveis();
        cont++;
    }
    cont = 0;
};








/*
De acordo com testes realizados pela equipe, constantemente o robo não identificava uma curva de 90 graus e ficava rotacionando para esquerda e direita entre as duas linhas na curva
    Como forma de resolver esse problema foi desenvolvido uma ideia onde o robo contará quantas vezes ele identificou preto em cada sensor
        Essa ideia foi separa em duas partes:

        - Parte A: SensorCorBranco
        Onde toda vez que o robo se alinhava com a linha preta (todos os sensores de cor, com exeção do meio, identificavam branco) era zerado o contador de linhas pretas identificadas

        - Parte B: AntBugCurva90Graus
        Onde era identificado que o robo localizou 2 vezes em cada sensor de cor a linha preta (indicando que ele estava rotacionando para esquerda e direita na curva de 90 graus), com isso ele realizava uma ré para que pudesse se realinhar na linha preta
    */

// Parte A
Action SensoresCorBranco = () => {

    AtualizarVariáveis();

    //Se alinha com a linha preta
    if(bc.returnColor(1) == "BRANCO" && bc.ReturnColor(3) == "BRANCO" && bc.returnColor(0) == "BRANCO" && bc.ReturnColor(4) == "BRANCO"){
        LigarLed("BRANCO");
        Estado = "Andando";
        
        Andar(VelocidadeBranco, VelocidadeBranco);
        Esperar(1);
        Parar();

        // Zera as variaveis utilizadas para identificar o bug da curva de 90 graus
        PretoPontaEsquerda = 0;
        PretoPontaDireita = 0;
        PretoDireita = 0;
        PretoEsquerda = 0;

        AtualizarVariáveis();

    }
};



//Parte B
Action AntBugCurva90Graus = () => {

    //Identifica que travou na curva de 90 graus
    if (PretoPontaDireita + PretoDireita >= 1 && PretoPontaEsquerda + PretoEsquerda >= 1){
        LigarLed( "PRETO" );
        Estado = "Travou na curva de 90 graus";       
        
        Andar(VelocidadeMaximaTras, VelocidadeMaximaTras);
        Esperar(70);
        Parar();

        // Zera as variaveis utilizadas para identificar o bug da curva de 90 graus
        PretoPontaEsquerda = 0;
        PretoPontaDireita = 0;
        PretoDireita = 0;
        PretoEsquerda = 0;


    } 
};
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//





// Anda em uma determinada velocidade na area de salvamento
Action AndaAreaSalvamento = () => {
    Estado = "Procurando área de resgate";

    Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
    Esperar(1);
};



// Estado onde todos os sensores de cor centrais identificam preto
Action EncruzilhadaPretaDupla = () => {

    //Idenfica uma encruzilhada preta dupla
    if(bc.ReturnColor(2) == "PRETO" && bc.returnColor(0) == "PRETO" && bc.ReturnColor(4) == "PRETO"){
        LigarLed("VERMELHO");

        Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
        Esperar(250);

        Parar();

        AtualizarVariáveis();

    }
};



Action TratamentoEncruzilhadaVerde = () => {

    AnguloZero = DireçãoDoRobô;

    /* 
    Para identificação das encruzilhadas separamos a angulação do robo em A (0 à 180) e B (180 à 360)
        Logo se a angulação do robo estiver no intervalo de B, o angulo do robo será convertido para o intervalo de A
            Isso facilitará a identificação de quanto o robo precisará dobrar e quanto será o angulo de sua dobra
    */

    //Se o angulo estiver em B
    if (AnguloZero > 180){

        //muda o angulo para um equivalente no intervalo de A
        AnguloZero = 360 - AnguloZero;
        Maior180 = true;
    }

    //Se o angulo estiver em A
    else {
        Maior180 = false;
    }

    //Gira para determinado lado para a não identificação da linha preta na frente da encruzilhada verde
    Rotacionar(VelocidadeCurva, (35 * SentidoCurva));

    AtualizarVariáveis();

    //Dobra para um determinado lado até o sensor de cor do meio virado para baixo identificar preto
    while(bc.ReturnColor(2) != "PRETO"){

        AtualizarVariáveis();

        Rotacionar(VelocidadeCurva, (0.01f * SentidoCurva));

        LigarLed("VERMELHO");
    }

    Parar();
    AtualizarVariáveis();

    //Chega a conclusão de que o robo esta na angulação ideal
    AnguloFinal = DireçãoDoRobô;
        
    //Iguala (em relação a proporcionalidade) o angulo ideal com o angulo atual do robo --> OBS: Isso so ocorre se a angulação do robo estiver em B
    if(Maior180){
        AnguloFinal = 360 - AnguloFinal;
    }
        
    //Conclui a quantidade final girada (em graus) que o robo realizou durante a ação
    QuantidadeGiro = AnguloFinal - AnguloZero;   
};



Action Tratamento90Graus = () => {

    /* 
    Para identificação das curvas de 90 graus separamos a angulação do robo em A (0 à 180) e B (180 à 360)
        Logo se a angulação do robo estiver no intervalo de B, o angulo do robo será convertido para o intervalo de A
            Isso facilitará a identificação de quanto o robo precisará dobrar e quanto será o angulo de sua dobra
    */

    AtualizarVariáveis();
    AnguloZero = DireçãoDoRobô;

    //Se o angulo estiver em B
    if (AnguloZero > 180){

        AnguloZero = 360 - AnguloZero;
        Maior180 = true;
    }

    //Se o angulo estiver em A
    else {
        Maior180 = false;
    }

    Rotacionar(VelocidadeCurva, (85 * SentidoCurva));

    AtualizarVariáveis();
    AnguloFinal = DireçãoDoRobô;

    //Iguala (em relação a proporcionalidade) o angulo ideal com o angulo atual do robo --> OBS: Isso so ocorre se a angulação do robo estiver em B 
    if(Maior180){
        AnguloFinal = 360 - AnguloFinal;
    }
        
    //Conclui a quantidade final girada (em graus) que o robo realizou durante a ação
    QuantidadeGiro = AnguloFinal - AnguloZero;    
};



// Alinha o robô de acordo com a angulação dele mais proxima da angulação das areas de resgate
Action AlinhaResgate = () => {

    AtualizarVariáveis();

    // 315 graus
    if (DireçãoDoRobô >= 300 && DireçãoDoRobô < 315){
        WhileAlinhamento(315, 0.1f);
    }
    else if (DireçãoDoRobô > 315 && DireçãoDoRobô <= 330){
        WhileAlinhamento(315, -0.1f);
    }

    //45 graus
    else if (DireçãoDoRobô >= 30 && DireçãoDoRobô < 45){
        WhileAlinhamento(45, 0.1f);
    }
    else if (DireçãoDoRobô > 45 && DireçãoDoRobô <= 60){
        WhileAlinhamento(45, -0.1f);
    }

    //135 graus
    else if (DireçãoDoRobô >= 120 && DireçãoDoRobô < 135){
        WhileAlinhamento(135, 0.1f);
    }
    else if (DireçãoDoRobô > 135 && DireçãoDoRobô <= 150){
        WhileAlinhamento(135, -0.1f);
    }

    //225 graus
    if (DireçãoDoRobô >= 240 && DireçãoDoRobô < 225){
        WhileAlinhamento(225, 0.1f);
    }
    else if (DireçãoDoRobô > 225 && DireçãoDoRobô <= 210){
        WhileAlinhamento(225, -0.1f);
    }
};



//Identifica uma curva em um angulo discreto em uma encruzilhada verde
Action IdentificadorCurva = () => {
    
    //Identifica se a encruzilhada é em um circulo 
    if (QuantidadeGiro >= 57 && QuantidadeGiro <= 63){
            
        Andar(VelocidadeMaximaTras, VelocidadeMaximaTras);
        Esperar(175);
        Parar();
        }

    /*
    As curvas que o robo deverá realizar durante o percurso na arena ficam restritas a determinados angulo -> 0/360, 90, 180, 270
        De tal forma, quando for identificado uma curva reta, o angulo atual do robo será comparado a essas 4 possibilidades (OBS: a margem de erro da angulação foi estabelecida em 15, tanto pra mais quanto pra menos)
            Assim, o robo será capaz de distinguir para qual direção girar e o quanto será necessario rotacionar para chegar a determinado angulo
    */

    if (QuantidadeGiro >= 80 && QuantidadeGiro <= 100){

        AtualizarVariáveis();

        //Se o angulo for 0/360
        if (DireçãoDoRobô >= 345 && DireçãoDoRobô < 360){
            WhileAlinhamento(0, 0.01f);
        }
        else if (DireçãoDoRobô > 0 && DireçãoDoRobô <= 15){
            WhileAlinhamento(0, -0.01f);
        }


        //Se o angulo for 90
        else if (DireçãoDoRobô >= 75 && DireçãoDoRobô < 90){
            WhileAlinhamento(90, 0.01f);
        }
        else if (DireçãoDoRobô > 90 && DireçãoDoRobô <= 105){
            WhileAlinhamento(90, -0.01f);
        }


        //Se o angulo for 180
        else if (DireçãoDoRobô >= 165 && DireçãoDoRobô < 180){
            WhileAlinhamento(180, 0.01f);
        }
        else if (DireçãoDoRobô > 180 && DireçãoDoRobô <= 195){
            WhileAlinhamento(180, -0.01f);
        }


        //Se o angulo for 270
        else if(DireçãoDoRobô >= 255 && DireçãoDoRobô < 270){
            WhileAlinhamento(270, 0.01f);
        }
        else if(DireçãoDoRobô > 270 && DireçãoDoRobô <= 285){
            WhileAlinhamento(270, -0.01f);
        }
    }
};



// Se alinha quando identifica uma curva de 90 graus para identificar se é uma intercecção
Action AlinhaAngulos = () => {
    
    AtualizarVariáveis();

    //Se o angulo for 0/360
    if (DireçãoDoRobô >= 350 && DireçãoDoRobô < 360) {WhileAlinhamento(0, 0.001f);}
    else if (DireçãoDoRobô > 0 && DireçãoDoRobô <= 10) {WhileAlinhamento(0, -0.001f);}


    //Se o angulo for 90
    else if (DireçãoDoRobô >= 80 && DireçãoDoRobô < 90) {WhileAlinhamento(90, 0.001f);}
    else if (DireçãoDoRobô > 90 && DireçãoDoRobô <= 100) {WhileAlinhamento(90, -0.001f);}


    //Se o angulo for 180
    else if (DireçãoDoRobô >= 170 && DireçãoDoRobô < 180) {WhileAlinhamento(180, 0.001f);}
    else if (DireçãoDoRobô > 180 && DireçãoDoRobô <= 190) {WhileAlinhamento(180, -0.001f);}


    //Se o angulo for 270
    else if(DireçãoDoRobô >= 260 && DireçãoDoRobô < 270) {WhileAlinhamento(270, 0.001f);}
    else if(DireçãoDoRobô > 270 && DireçãoDoRobô <= 280) {WhileAlinhamento(270, -0.001f);}


    // 315 graus
    if (DireçãoDoRobô >= 300 && DireçãoDoRobô < 315) {WhileAlinhamento(315, 0.1f);}
    else if (DireçãoDoRobô > 315 && DireçãoDoRobô <= 330) {WhileAlinhamento(315, -0.1f);}


    //45 graus
    else if (DireçãoDoRobô >= 30 && DireçãoDoRobô < 45) {WhileAlinhamento(45, 0.1f);}
    else if (DireçãoDoRobô > 45 && DireçãoDoRobô <= 60) {WhileAlinhamento(45, -0.1f);}


    //135 graus
    else if (DireçãoDoRobô >= 120 && DireçãoDoRobô < 135) {WhileAlinhamento(135, 0.1f);}
    else if (DireçãoDoRobô > 135 && DireçãoDoRobô <= 150) {WhileAlinhamento(135, -0.1f);}

    //225 graus
    if (DireçãoDoRobô >= 240 && DireçãoDoRobô < 225) {WhileAlinhamento(225, 0.1f);}
    else if (DireçãoDoRobô > 225 && DireçãoDoRobô <= 210) {WhileAlinhamento(225, -0.1f);}
    
};



// Se alinha em um angulo discreto
Action AlinhaCurva = () => {

 AtualizarVariáveis();

    //Se o angulo for 0/360
    if (DireçãoDoRobô >= 330 && DireçãoDoRobô < 360){
        WhileAlinhamento(0, 0.001f);
    }
    else if (DireçãoDoRobô > 0 && DireçãoDoRobô <= 30){
        WhileAlinhamento(0, -0.001f);
    }


    //Se o angulo for 90
    else if (DireçãoDoRobô >= 60 && DireçãoDoRobô < 90){
        WhileAlinhamento(90, 0.001f);
    }
    else if (DireçãoDoRobô > 90 && DireçãoDoRobô <= 120){
        WhileAlinhamento(90, -0.001f);
    }


    //Se o angulo for 180
    else if (DireçãoDoRobô >= 150 && DireçãoDoRobô < 180){
        WhileAlinhamento(180, 0.001f);
    }
    else if (DireçãoDoRobô > 180 && DireçãoDoRobô <= 210){
        WhileAlinhamento(180, -0.001f);
    }


    //Se o angulo for 270
    else if(DireçãoDoRobô >= 240 && DireçãoDoRobô < 270){
        WhileAlinhamento(270, 0.001f);
    }
    else if(DireçãoDoRobô > 270 && DireçãoDoRobô <= 300){
        WhileAlinhamento(270, -0.001f);
    }

};



// Dobra uma angulação determinada na area de salvamento
Action DobraAreaSalvamento = () => {
    Estado = "Parede encontrada";

    Rotacionar(VelocidadeCurva, AnguloDobraAreaSalvamento);
    AlinhaCurva();
};



Action VarreduraAreaSalvamento = () => {
    
    /*
        Para otimizar o metodo de resgatar a vitimas, chegamos a conclusão de que é essencial a coleta precisa das mesmas
            Para isso fizemos um sistema que através da distancia da vitima em relação a parede o robô será capaz de busca-lá com a maior precisão
                Quando o robô identificar a vitima ele irá girar até parar de ver a vitima
                Após fazer isso o robô irá voltar dividindo a angulação rodada por 2
                Dessa forma, ele "encaixará" a vitima no centro do atuador, ou seja, o robõ irá pegar a vitima com uma grande precisão
    */

    // Determina o campo de visão do robô para a identificação de vitimas
    while(bc.distance(2) > 300 || SensorDistanciaFrenteCimaSemDiferença >= (bc.distance(2) - 30) && SensorDistanciaFrenteCimaSemDiferença <= (bc.distance(2) + 5) || Vermelho >= 10 && Vermelho <= 13 ||
    bc.distance(0) > 900 && bc.distance(2) >= 122 && bc.distance(2) <= 126 || 
    bc.distance(0) > 900 && bc.distance(2) >= 211 && bc.distance(2) <= 215 ||
    bc.distance(0) > 900 && bc.distance(2) >= 186 && bc.distance(2) <= 192 ||
    bc.distance(0) > 900 && bc.distance(2) >= 275 && bc.distance(2) <= 283 
    ){
    
    // Atribui os dados encontrados pelos sensores nas determinadas variaives destes dados
    AtualizarVariáveis();
    
    // Escreve na tela o retorno dos sensores, onde na linha 1 são os sensores de cor, a linha 2 são os sensores de distancia, a linha 3 é o grau de inclunação em relação ao solo e o estado atual
    EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");
    EscreverNaTela(2,"SDFC_SD: " + SensorDistanciaFrenteCimaSemDiferença + " | SDFB: " + bc.distance(2));
    EscreverNaTela(3, "Fazendo: " + Estado);

    Rotacionar(VelocidadeCurva, 0.01f);
       
    }
    
    AnguloZero = Direção();

    // Com as distancias dos sensores de distancia da frente, será feito a rotação até que o robô pare de identificar a vitima
    while((bc.Distance(0) - 6.5f) >= (bc.Distance(2) + 2)){
        
    LigarLed("VERMELHO");
            
    // Escreve na tela o retorno dos sensores, onde na linha 1 são os sensores de cor, a linha 2 são os sensores de distancia, a linha 3 é o grau de inclunação em relação ao solo e o estado atual
    EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");
    EscreverNaTela(2,"SDFC_SD: " + SensorDistanciaFrenteCimaSemDiferença + " | SDFB: " + bc.distance(2));
    EscreverNaTela(3, $"Inclinação: {bc.Inclination()} | Fazendo: " + Estado);

    Rotacionar(VelocidadeCurva, 0.01f);

        if(bc.returnRed(IDSensorCorFrente) >= 10 && bc.returnRed(IDSensorCorFrente) <= 13){
            break;
        }
    }

    EscreverNaTela(3, "Angulação da posição da vitima: " + GiroIdentificaçãoVitima.ToString() + " | " + "Fazendo: " + Estado);

    // Calcula o quanto o robô precisa voltar para ficar em frente a vitima e pega-lá
    AnguloFinal = Direção();
        
    if (AnguloZero > 180 && AnguloFinal < 180){
        
        AnguloZero = AnguloZero - 180;
        AnguloFinal = AnguloFinal + 180;

    }
    
    QuantidadeGiro = AnguloFinal - AnguloZero; 

    // Com a angulação ideal do robô calculada ele irá rotacionar esse angulo e ficar alinhado com a vitima
    EscreverNaTela(3, "Angulos: " + AnguloZero.ToString() + " | " + AnguloFinal.ToString());

    Rotacionar(VelocidadeCurva, (-QuantidadeGiro / 2f));

};



// O rbô ficará centralizado em frente a area de resgate
Action AlinhaAreaSalvamento = () => {   
    
    while(bc.distance(0) > DistanciaAlinhaArea){
            AtualizarVariáveis();

            Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
            Esperar(1);
            Parar();
    }
    
    while(bc.distance(0) < DistanciaAlinhaArea){
            AtualizarVariáveis();
            
            Andar(VelocidadeMaximaTras, VelocidadeMaximaTras);
            Esperar(1);
            Parar();
    }  
};



// Converte RGB em HSV sem a luminosidade
Func<int, double> RGB2HSV = (Sensor) => {

    //converte o codigo de cor RGB para HSV através de uma regra de 3
    R = bc.returnRed(Sensor) / 100;
    B = bc.returnBlue(Sensor) / 100;
    G = bc.returnGreen(Sensor) / 100;

    //Calcula o H(Hue) do HSV como forma de identificar a faixa prateada da area de salvamento

    // Primeiro caso -> MAX = R && G >= B
    if(R > G && G >= B){ 
        H = 60 * (G - B)/(R - B) + 0;

    }

    // Segundo caso -> MAX = R && G < B
    if(R > B && G < B){
        H = 60 * (G - B)/(R - G) + 360;

    }

    // Terceiro caso -> MAX = G
    if(G > R && G > B){
        // MIN = B
        if (B < R){
            H = 60 * (B - R)/(G - B) + 120;

        }
        // MIN = R
        if (R <= B){
            H = 60 * (B - R)/(G - R) + 120;

        }     
    }

    // Quarto caso -> MAX = B
    if(B > R && B > G){
        // MIN = R
        if (R < G){
        H = 60 * (R - G)/(B - R) + 240;

        }
        // MIN = G
        if (G <= R){
            H = 60 * (R - G)/(B - G) + 240;

        }       
    }
    return H;
};







// Quando os dois sensores de cor identificam preto do mesmo lado ele dobra 90 graus para determinado lado
Action Estado_2 = () => {       

    EncruzilhadaPretaDupla();
    AtualizarVariáveis();

    if (bc.ReturnColor(3) ==  "PRETO"  && bc.ReturnColor(4) ==  "PRETO" ){
        // Estado 2,1 -> Curva de 90 graus na linha preta (ESQUERDA)
        LigarLed( "PRETO" );
        Estado = "Curva de 90° Esquerda"; 
        
        AndarPorRotaçoes(VelocidadeMaximaFrente, 6);
        Parar();     


        AlinhaCurva();

        Parar();

 

        //Identifica que é uma curva preta para a esquerda
        ZerarTemporizador();
        while (bc.ReturnColor(2) != "PRETO" && bc.returnColor(1) != "PRETO" && bc.ReturnColor(3) != "PRETO" && Temporizador() < 300){
            AtualizarVariáveis();
            Rotacionar(VelocidadeCurva, 0.01f);
        }
        
        if(bc.ReturnColor(2) != "PRETO" && bc.returnColor(1) != "PRETO" && bc.ReturnColor(3) != "PRETO" ){
            Rotacionar(VelocidadeCurva, -12);
            AlinhaAngulos();

            LigarLed("VERMELHO");

            AndarPorRotaçoes(VelocidadeMaximaFrente, 6);

            AtualizarVariáveis();

            Parar();

            SentidoCurva = -1;
            Tratamento90Graus();
            IdentificadorCurva();

            AndarPorRotaçoes(VelocidadeMaximaTras, 9);
            Parar();
            
        }
        else{
            Parar();
        }
        AtualizarVariáveis();
    }
    if (bc.returnColor(1) ==  "PRETO"  && bc.returnColor(0) ==  "PRETO" ) {      
        // Estado 2,2 -> Curva de 90 graus na linha preta (DIREITA)
        LigarLed( "PRETO" );
        Estado = "Curva de 90° Direita";       
        
        AndarPorRotaçoes(VelocidadeMaximaFrente, 6);
        Parar();
        
        AtualizarVariáveis();

        AlinhaAngulos();
        
        AtualizarVariáveis();

        Parar();     


        //Identifica que é uma curva preta para a direita
        ZerarTemporizador();
        while (bc.ReturnColor(2) != "PRETO" && bc.returnColor(1) != "PRETO" && bc.ReturnColor(3) != "PRETO" && Temporizador() < 300){
            AtualizarVariáveis();
            Rotacionar(VelocidadeCurva, 0.01f);
        }

        if(bc.ReturnColor(2) != "PRETO" && bc.returnColor(1) != "PRETO" && bc.ReturnColor(3) != "PRETO"){
            Rotacionar(VelocidadeCurva, -12);
            AlinhaAngulos();

            LigarLed("VERMELHO");

            AndarPorRotaçoes(VelocidadeMaximaFrente, 6);
            AtualizarVariáveis();

            Parar();

            SentidoCurva = 1;
            Tratamento90Graus();
            IdentificadorCurva();

            AndarPorRotaçoes(VelocidadeMaximaTras, 9);
            Parar();

        }else{
            Parar();
        }
        AtualizarVariáveis();
    }
    AtualizarVariáveis();    
};







// Dobra para determinado lado quando um sesnsor identifica preto
Action Estado_1 = () => {

    AtualizarVariáveis();

    if(bc.returnColor(0) == "PRETO"){
        // Estado 1,5 -> Identificador de linha preta(PONTADIREITA)   
        LigarLed( "PRETO" );
         Estado = "Dobrar Ponta Direita";
        Parar();

        Rotacionar(VelocidadeCurva, 20);

        Estado_2();

        //Conclui que a linha preta foi identificada mais uma vez pelo sensor
        PretoPontaDireita = PretoPontaDireita + 1;

    }
    if (bc.ReturnColor(4) == "PRETO") { 
        // Estado 1,4 -> Identificador de linha preta (PONTAESQUERDA) 
        LigarLed( "PRETO" );
        Estado = "Dobrar Ponta Esquerda"; 
        Parar();

        Rotacionar(VelocidadeCurva, -20);

        Estado_2();

        //Conclui que a linha preta foi identificada mais uma vez pelo sensor
        PretoPontaEsquerda = PretoPontaEsquerda + 1;

    }
    if (bc.ReturnColor(3) == "PRETO") { 
         // Estado 1,2 -> Identificador de linha preta (ESQUERDA) 
        LigarLed( "PRETO" );
        Estado = "Dobrar Esquerda";
        Parar();

        Rotacionar(VelocidadeCurva, -0.20f);

        Estado_2();

        //Conclui que a linha preta foi identificada mais uma vez pelo sensor
        PretoEsquerda = PretoEsquerda + 1;
      
    }
    if (bc.returnColor(1) == "PRETO") {
         // Estado 1,3 -> Identificador de linha preta (DIREITA) 
        LigarLed( "PRETO" );
        Estado = "Dobrar Direita";
        Parar();

        Rotacionar(VelocidadeCurva, 0.20f);

        Estado_2();

        //Conclui que a linha preta foi identificada mais uma vez pelo sensor
        PretoDireita = PretoDireita + 1;
    }

    SensoresCorBranco();

    AntBugCurva90Graus();

};







// Pega a vitima
Action Estado_10 = () => {
    
    //Gira o atuador (tanto a posição do braço quanto o angulo do atuador) em uma determinada angulação
    VelocidadeAtuador(150);
    AtuadorBaixo(560);
    GirarAtuadorCima(140);

    Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
    Esperar(1000);
    AtuadorCima(300);
    Parar();
        
    //Gira o atuador (tanto a posição do braço quanto o angulo do atuador) em uma determinada angulação
    AtuadorCima(230);
    GirarAtuadorBaixo(650);  // Proporção 150

    Rotacionar(VelocidadeCurva, 150);
};








// Entrega a vitima na area de resgate
Action Estado_11 = () => {
    
    Estado = "Vítima salva";                                    
                                    
    Parar();

    VelocidadeAtuador(150);

    //Gira o atuador (tanto a posição do braço quanto o angulo do atuador) em uma determinada angulação
    AtuadorBaixo(400);
    GirarAtuadorBaixo(500);
    
    Esperar(300);



    //Gira o atuador (tanto a posição do braço quanto o angulo do atuador) em uma determinada angulação
    AtuadorCima(400);
    GirarAtuadorCima(500);
    
    // Identifica a area de resgate
    while(Vermelho >= 10 && Vermelho <= 13 || SensorDistanciaFrenteCimaSemDiferença >= (bc.distance(2) + 5)){

        AtualizarVariáveis();

        EscreverNaTela(2,"SDFC_SD: " + SensorDistanciaFrenteCimaSemDiferença + " | SDFB: " + bc.distance(2));
        Rotacionar(VelocidadeCurva, 0.1f);

    }   
    Rotacionar(VelocidadeCurva, 5);

    AlinhaResgate();
    AlinhaAreaSalvamento();

    Parar();
    Rotacionar(VelocidadeCurva, 10); 
};                                                                      







// Inicia a varredura das vitimas girando no seu proprio eixo na lateral da area de resgate 
Action Estado_9 = () => {

    VarreduraAreaSalvamento();

    //Estado 9.2 -> Identifica uma vitima em uma distancia menor que 25
    if(bc.distance(2) < 25){
        Estado = "Vítima perto encontrada";
        
        EscreverNaTela(2,"SDFC_SD: " + SensorDistanciaFrenteCimaSemDiferença + " | SDFB: " + bc.distance(2));
                                
        Parar();

        AndarPorRotaçoes(VelocidadeVitimaMenor25Frente, Convert.ToSingle((bc.distance(2) + 10) / 2));
        AndarPorRotaçoes(VelocidadeVitimaMenor25Tras, Convert.ToSingle((bc.distance(2) + 10) / 2));

        Esperar(1000);

        AtualizarVariáveis();

        Parar();
        Rotacionar(VelocidadeCurva, 10);
     
        AtualizarVariáveis();
        
        DistanciaVitimaMenor25 = true;
    }

    //Estado 9.3 -> Identifica uma vitima em uma distancia maior ou igual a que 25
    else{
        Estado = "Vítima encontrada";

        AndarPorRotaçoes(VelocidadeMaximaFrente, Convert.ToSingle((bc.distance(2) - 35) / 2));
        Parar();
    }
    
    //Se o robo tiver identificado uma vitima à uma distancia menor que 25, ele reiniciará a varredura em busca de outras vitimas
    if (DistanciaVitimaMenor25 == true){
        
        AtualizarVariáveis();
        Esperar(400);

        VarreduraAreaSalvamento();

        AndarPorRotaçoes(VelocidadeMaximaFrente, Convert.ToSingle((bc.distance(2) - 30) / 2));
        Parar();

        DistanciaVitimaMenor25 = false;
    }

    AtualizarVariáveis();
    Estado = "Vitima resgatada";
};







// Chega no topo da rampa e inicia a varredura, se guiando pelas paredes, para localizar a area de resgate OBS: INCLUI OS ESTADOS 9, 10, 11
Action Estado_8 = () => {

    //Robo identifica a area de resgate 
    if(Vermelho >= 10 && Vermelho <= 12){

        Estado = "Área de resgate encontrada";

        Parar();

        AtualizarVariáveis();             
        AlinhaCurva();
        
        Rotacionar(VelocidadeCurva, 45);
        AlinhaAreaSalvamento();
        
        //Se posiciona no meio da area de resgate para iniciar varredura das vitimas
        AlinhaResgate();        
        AlinhaAreaSalvamento();

        Parar();
        Rotacionar(VelocidadeCurva, 10);       
        

        //Inicia a varredura e o resgate das vitimas
        while(true){

            AtualizarVariáveis();

            Estado_9 ();

            Estado_10 ();
                            
            while(true){

                // Atribui os dados encontrados pelos sensores nas determinadas variaives destes dados
                AtualizarVariáveis();
    
                // Escreve na tela o retorno dos sensores, onde na linha 1 são os sensores de cor, a linha 2 são os sensores de distancia, a linha 3 é o grau de inclunação em relação ao solo e o estado atual
                EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");
                EscreverNaTela(2,$"SDFC: {bc.distance(0)} | SDFB: {bc.distance(2)} | SDL: {bc.Distance(1)}");
                EscreverNaTela(3, $"Inclinação: {bc.Inclination()} | Fazendo: " + Estado);

                //Dobra quando encontra a parede
                if(bc.distance(0) <= 40){

                    DobraAreaSalvamento();
                }
                                
                //Estado 11 -> Entrega as vitimas na area de resgate
                if(Vermelho >= 10 && Vermelho <= 12){                                  
                                    
                    Estado_11 ();

                    break;                                                      
                }
                else{
                    
                     AtualizarVariáveis();

                    if (bc.distance(2) <= 5 && bc.distance(0) > 5){

                        AtualizarVariáveis();
                        Rotacionar(VelocidadeMaximaFrente, 30);
                    }
                    
                    AndaAreaSalvamento();
                }            
            }
        }
    }

    AtualizarVariáveis();
    
    //Dobra quando encontra a parede
    if(bc.distance(0) < 25){
        DobraAreaSalvamento();      

    }else{

        AtualizarVariáveis();
        AndaAreaSalvamento();
    }
        
};

bool achouAreaDireita = false;
bool achouAreaEsquerda = false;
bool achouAreaExEsquerda = false;

void ForArea(int rotação, string achouArea){
    for (int i = 0; i < 10; i++){
            EscreverNaTela(2, $"SDFC: {bc.distance(0)} | SDFB: {bc.distance(2)} | SDL: {bc.Distance(1)}");

            if(bc.Distance(2) > 90 && bc.Distance(2) < 125){
                EscreverNaTela(0, "Área ou bolinha?");
                float dist = (bc.distance(2)/2) -3;

                AndarPorRotaçoes(VelocidadeMaximaFrente, dist);   

                if(bc.returnRed(5) >= 0 && bc.returnRed(5) <= 5){
                    if(achouArea == "direita"){
                        achouAreaDireita = true;
                    } else if(achouArea == "esquerda"){
                        achouAreaEsquerda = true;
                    }else {
                        achouAreaExEsquerda = true;
                    }
                    EscreverNaTela(0, "Área");
                    if(bc.HasVictim()){
                        bc.ActuatorDown(700);
                        Esperar(500);
                        bc.ActuatorUp(700);
                    }
                    break;
                } else if(bc.ReturnRed(5) >= 69 && bc.ReturnRed(5) <= 71){
                    EscreverNaTela(0, "Parede");
                    AndarPorRotaçoes(VelocidadeMaximaTras, dist); 
                    bc.MoveFrontalAngles(VelocidadeCurva, -40);
                } else{
                    if(!bc.HasVictim()){
                        EscreverNaTela(0, "Bolinha");
                        bc.OpenActuator();
                        AndarPorRotaçoes(VelocidadeMaximaTras, 5);
                        bc.ActuatorDown(700);
                        AndarPorRotaçoes(VelocidadeMaximaFrente, 7);
                        bc.CloseActuator();
                        bc.ActuatorUp(700);
                        AndarPorRotaçoes(VelocidadeMaximaFrente, 3);
                        AndarPorRotaçoes(VelocidadeMaximaTras, 5);
                        AndarPorRotaçoes(VelocidadeMaximaTras, dist); 
                        Parar();
                        bc.MoveFrontalAngles(VelocidadeCurva, -50);
                        AlinhaCurva();
                    } else{
                        AndarPorRotaçoes(VelocidadeMaximaTras, dist);
                        Rotacionar(VelocidadeCurva, -3); 
                    }
                }                
                
            } else if(bc.Distance(2) > 180){
                EscreverNaTela(0, "Canto da quadra");

                bc.MoveFrontalAngles(VelocidadeCurva, (rotação * (40 + i)));
                AlinhaCurva();
                break;

            } else if(bc.Distance(2) > 30 && bc.Distance(2) < 100){
                float dist = (bc.distance(2)/2);

                EscreverNaTela(0, "Bolinha na frente");

                bc.ActuatorDown(700);
                bc.OpenActuator();
                AndarPorRotaçoes(VelocidadeMaximaFrente, dist);  
                Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
                bc.CloseActuator();
                bc.ActuatorUp(700);
                Esperar(200);
                Parar();
                AndarPorRotaçoes(VelocidadeMaximaTras, dist); 
                Andar(VelocidadeMaximaTras, VelocidadeMaximaTras);
                Esperar(700);
                Parar();
                
                EscreverNaTela(2, $"SDFC: {bc.distance(0)} | SDFB: {bc.distance(2)} | SDL: {bc.Distance(1)}");


            } else if(bc.Distance(2) < 30 && bc.Distance(2) > 5){
                float dist = (bc.distance(2)/2) -3;

                EscreverNaTela(0, "Bolinha muito perto");

                AndarPorRotaçoes(VelocidadeMaximaTras, dist); 
                bc.ActuatorDown(700);
                bc.OpenActuator();
                AndarPorRotaçoes(VelocidadeMaximaFrente, (dist +7));
                bc.CloseActuator();
                bc.ActuatorUp(700);
                AndarPorRotaçoes(VelocidadeMaximaTras, 7);

            }
            
    }
};



Action BuscarVitimas = () => {
    bc.MoveFrontalAngles(VelocidadeCurva, 135);
    AlinhaCurva();
    while(true){
    while(bc.Distance(1) >= 180){
        EscreverNaTela(0, "Buscando vitimas");
        Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);

        // Bolinha na frente
        if((bc.Distance(2) +7) <= bc.Distance(0)){
            EscreverNaTela(0, "Bolinha encontrada");
            AlinhaCurva();  
            float dist = (bc.distance(2)/2);
            if(bc.Distance(2) <= 25){
                EscreverNaTela(0, "Bolinha muito perto");

                AndarPorRotaçoes(VelocidadeMaximaTras, (dist + 1));
                bc.ActuatorDown(700);
                bc.OpenActuator();
                AndarPorRotaçoes(VelocidadeMaximaFrente, (dist +5));
                bc.CloseActuator();
                bc.ActuatorUp(700);
                Esperar(50);
                AndarPorRotaçoes(VelocidadeMaximaTras, 2);
                bc.MoveFrontalAngles(VelocidadeCurva, 180);
                AlinhaCurva();  
                while(bc.ReturnRed(5) > 3){
                    AndarPorRotaçoes(VelocidadeMaximaFrente, 2);
                }
                bc.ActuatorDown(700);
                bc.TurnActuatorDown(300);
                while(bc.HasVictim()){
                    Esperar(10);
                }
                bc.TurnActuatorUp(300);
                bc.ActuatorUp(700);
                bc.MoveFrontalAngles(VelocidadeCurva, 180);
                AlinhaCurva(); 
            }else{
                bc.ActuatorDown(700);
                bc.OpenActuator();
                AndarPorRotaçoes(VelocidadeMaximaFrente, dist);  
                Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
                bc.CloseActuator();
                bc.ActuatorUp(700);
                Esperar(50);
                Parar();
                AndarPorRotaçoes(VelocidadeMaximaTras, dist); 
                Andar(VelocidadeMaximaTras, VelocidadeMaximaTras);
                Esperar(900);
                bc.MoveFrontalAngles(VelocidadeCurva, 180);
                AlinhaCurva();  
                Parar();
                while(bc.ReturnRed(5) > 3){
                    AndarPorRotaçoes(VelocidadeMaximaFrente, 2);
                }
                bc.ActuatorDown(700);
                bc.TurnActuatorDown(300);
                while(bc.HasVictim()){
                    Esperar(10);
                }
                bc.TurnActuatorUp(300);
                bc.ActuatorUp(700);
                bc.MoveFrontalAngles(VelocidadeCurva, 180);
                AlinhaCurva(); 
            }
        }
        // Chegou na parede da arena
        if(bc.Distance(0) <= 20){
            EscreverNaTela(0, "Fim da linha");
            Parar();
        }
    }
    AndarPorRotaçoes(VelocidadeMaximaFrente, 2);
    bc.MoveFrontalAngles(VelocidadeCurva, 90);

    // Bolinha do lado
    if((bc.Distance(2) +7) <= bc.Distance(0)){
        EscreverNaTela(0, "Bolinha encontrada");
        AlinhaCurva();  
        float dist = (bc.distance(2)/2);
        if(bc.Distance(2) <= 25){
            EscreverNaTela(0, "Bolinha muito perto");

            AndarPorRotaçoes(VelocidadeMaximaTras, dist); 
            bc.ActuatorDown(700);
            bc.OpenActuator();
            AndarPorRotaçoes(VelocidadeMaximaFrente, (dist +7));
            bc.CloseActuator();
            bc.ActuatorUp(700);
            Esperar(200);
            AndarPorRotaçoes(VelocidadeMaximaTras, 7);
            bc.MoveFrontalAngles(VelocidadeCurva, 90);

            while(bc.ReturnRed(5) > 3){
                AndarPorRotaçoes(VelocidadeMaximaFrente, 2);
            }
            bc.ActuatorDown(700);
            bc.TurnActuatorDown(300);
            while(bc.HasVictim()){
                Esperar(10);
            }
            bc.TurnActuatorUp(300);
            bc.ActuatorUp(700);
            bc.MoveFrontalAngles(VelocidadeCurva, 180);
            AlinhaCurva();  
        }else{
            bc.ActuatorDown(700);
            bc.OpenActuator();
            AndarPorRotaçoes(VelocidadeMaximaFrente, dist);  
            Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
            bc.CloseActuator();
            bc.ActuatorUp(700);
            Esperar(200);
            Parar();
            AndarPorRotaçoes(VelocidadeMaximaTras, dist); 
            Andar(VelocidadeMaximaTras, VelocidadeMaximaTras);
            Esperar(900);
            bc.MoveFrontalAngles(VelocidadeCurva, 90);
            Parar();

            while(bc.ReturnRed(5) > 3){
                AndarPorRotaçoes(VelocidadeMaximaFrente, 2);
            }
            bc.ActuatorDown(700);
            bc.TurnActuatorDown(300);
            while(bc.HasVictim()){
                Esperar(10);
            }
            bc.TurnActuatorUp(300);
            bc.ActuatorUp(700);
            bc.MoveFrontalAngles(VelocidadeCurva, 180);
            AlinhaCurva();  
        }
    }
    }

};



// Identifica a rampa para a area de salvamento
Action Estado_7 = () => {
    

    //Identifica a rampa da area de salvamento 
    if(bc.distance(1) <= 40 && bc.distance(1) >= 30 && bc.inclination() >= 330 && bc.inclination() <= 344){
        //Prepara para área de resgate
        Estado = "Área de salvamento";
        LigarLed("AMARELO");
        AlinhaCurva();
        while(bc.distance(1) <= 40 && bc.distance(1) >= 27){
            EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");
            EscreverNaTela(2,$"SDFC: {bc.distance(0)} | SDFB: {bc.distance(2)} | SDL: {bc.Distance(1)}");
            EscreverNaTela(3, $"Inclinação: {bc.Inclination()} | Fazendo: " + Estado);
            
            Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
        }


        Estado = "Verificando posição da área de resgate";
        // Escreve na tela o retorno dos sensores, onde na linha 1 são os sensores de cor, a linha 2 são os sensores de distancia, a linha 3 é o grau de inclunação em relação ao solo e o estado atual
        EscreverNaTela(1, $"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");
        EscreverNaTela(2, $"SDFC: {bc.distance(0)} | SDFB: {bc.distance(2)} | SDL: {bc.Distance(1)}");
        EscreverNaTela(3, $"Inclinação: {bc.Inclination()} | Fazendo: " + Estado);
        
        Parar();
        AlinhaCurva();
        //Identifica cada elemento da area de resgate (NAS LATERAIS)
        
        while(bc.Distance(0) > 220){
           Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
               
        }        
        Parar();
        AlinhaCurva();
        bc.MoveFrontalAngles(VelocidadeCurva, 45);
        AlinhaAngulos();
        Parar();
        AndarPorRotaçoes(VelocidadeMaximaFrente, 70);
        Parar();
        Rotacionar(VelocidadeCurva, 45);
        AlinhaCurva();
        EscreverNaTela(0, $"{bc.Distance(0)}");

        while(bc.distance(0) > 133){
            EscreverNaTela(0, $"{bc.Distance(0)}");
            Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
        }
        
        while(bc.distance(0) < 135){
            EscreverNaTela(0, $"{bc.Distance(0)}");
            Andar(VelocidadeMaximaTras, VelocidadeMaximaTras);
        }
        Parar();
 
        bc.MoveFrontalAngles(VelocidadeCurva, 40);
        ForArea(-1, "direita");


        if(achouAreaDireita == false){
            EscreverNaTela(1, "Área para a direita não encontrada");
            bc.MoveFrontalAngles(VelocidadeCurva, -40);
            ForArea(+1, "esquerda");
            AlinhaAngulos();
            if(achouAreaEsquerda == false){
                EscreverNaTela(1, "Área para a esquerda não encontrada");
                bc.MoveFrontalAngles(VelocidadeCurva, -80);
                ForArea(+1, "exEsquerda");
                

                if(achouAreaExEsquerda == false){
                    EscreverNaTela(1, "Área para a extrema esquerda não encontrada");
                    Esperar(10000);
                }

            } 
        }
        if(achouAreaEsquerda == true){
            EscreverNaTela(1, "Área para a esquerda encontrada");
            BuscarVitimas();
        }
        
        if(achouAreaDireita == true){
            EscreverNaTela(1, "Área para a direita encontrada");
            BuscarVitimas();        
        }
        if(achouAreaExEsquerda == true){
            EscreverNaTela(1, "Área para a extrema esquerda encontrada");
            BuscarVitimas();
        }
    }
};







// Desvia do obstaculo fazendo um formato de triângulo

/*
De acordo com testes realizados pela equipe, a melhor opção para desviar do obstaculo seria um triangulo
Primeiramente o robo se alinharia com o obstaculo utilizando o sensor de distancia lateral (o que evitaria uma grande variação no angulo inicial do estado, proviniente de curvas e encruzilhadas)
Ele ultrapassaria o obstaculo realizando uma curva de 56 graus
Para se alinhar novamente com a linha preta, o robo andaria para frente até identificar a faixa preta na frente do obstaculo com os sensores de cor determinados
O robo andaria por um determinado tempo para trás, identificando se o obstaculo estaria proximo nesse meio tempo com o sensor de toque traseiro
*/

Action Estado_6 = () => {    
    // Identifica o obstaculo
    if (bc.distance(2) <= DistanciaObstaculo && bc.ReturnColor(5) != "PRETO"){

        LigarLed("VERMELHO");
        Estado = "Desviando do Obstáculo";
        EscreverNaTela(3, "Fazendo: " + Estado);

        AlinhaCurva();  
        AndarPorRotaçoes(VelocidadeMaximaTras, 9);
        Rotacionar(VelocidadeCurva, 35);
        
        Andar(300, 0);
        Esperar(500);
        Andar(300, -95);
        Esperar(3500);
        Andar(1000, -1000);
        Esperar(2300);
        AndarPorRotaçoes(VelocidadeMaximaFrente, 3);


        // Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
        // Esperar(850);
        // Parar();

        // Rotacionar(VelocidadeCurva, -20);
        // AlinhaCurva();
        // Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
        // Esperar(650);
        // Rotacionar(VelocidadeCurva, -35);
        ZerarTemporizador();

        while(bc.ReturnColor(3) != "PRETO" && Temporizador() < 4500){Andar(VelocidadeObstaculo, VelocidadeObstaculo);}

        Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
        Esperar(550);

        
        Rotacionar(VelocidadeCurva, 35);
        AlinhaCurva();
        ZerarTemporizador();

        while(!bc.Touch(0) && Temporizador() < 1200){Andar(-200, -200);}
    }
};







// Dobra 60 graus para determinado lado quando um sesnsor identifica verde
Action Estado_3 = () => {

    AtualizarVariáveis();

    //Verifica se não é apenas uma sujeira na pista
    if (RGB2HSV(IDSensorCorPontaDireita) >= 107 && RGB2HSV(IDSensorCorPontaDireita) <= 114 || 
        RGB2HSV(IDSensorCorDireita) >= 107 && RGB2HSV(IDSensorCorDireita) <= 114 || 
        RGB2HSV(IDSensorCorEsquerda) >= 107 && RGB2HSV(IDSensorCorEsquerda) <=  114 || 
        RGB2HSV(IDSensorCorPontaEsquerda) >= 107 && RGB2HSV(IDSensorCorPontaEsquerda) <= 114)
    {

        LigarLed("AMARELO");
        Esperar(200);

        Andar(VelocidadeMaximaFrente, VelocidadeMaximaFrente);
        Esperar(50);
        Parar();

        AtualizarVariáveis();
        AlinhaAngulos();
        EscreverNaTela(0, $"Esquerda: {RGB2HSV(IDSensorCorEsquerda)} Direita: {RGB2HSV(IDSensorCorDireita)}");
        //OBS: MESMA LOGICA DOS ESTADOS TratamentoEncruzilhadaVerde() e IdentificadorCurva()
        if (bc.ReturnColor(1) == "VERDE" && bc.ReturnColor(3) == "VERDE"){
            EscreverNaTela(0, $"Esquerda: {RGB2HSV(IDSensorCorEsquerda)} Direita: {RGB2HSV(IDSensorCorDireita)}");
            Estado = "Beco sem saída";
            EscreverNaTela(3, "Fazendo: " + Estado);
            EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");
            bc.MoveFrontalAngles(VelocidadeCurva, 180);
            AlinhaCurva();
            AndarPorRotaçoes(VelocidadeMaximaTras, 9);
            AlinhaCurva();
        }

        if (bc.ReturnColor(3) == "VERDE"){
            // Estado 3,1 -> Identificador de encruzilhada verde (ESQUERDA)
            LigarLed("VERDE");
            Estado = "Curva Verde Esquerda";
            EscreverNaTela(3, "Inclinação: " + bc.inclination() + " | " + "Fazendo: " + Estado);    
            EscreverNaTela(0, $"Esquerda: {RGB2HSV(IDSensorCorEsquerda)} Direita: {RGB2HSV(IDSensorCorDireita)}");
            AndarPorRotaçoes(VelocidadeMaximaFrente, DistanciaAlinhamentoVerde);
            Parar();

            AtualizarVariáveis();
        
            EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");

            SentidoCurva = -1;
        
            TratamentoEncruzilhadaVerde();

            IdentificadorCurva();
            AtualizarVariáveis();
        }

        if(bc.ReturnColor(1) == "VERDE"){
            // Estado 3,2 -> Identificador de encruzilhada verde(DIREITA)   
            LigarLed("VERDE");
            Estado = "Curva Verde Direita";
            EscreverNaTela(3, "Inclinação: " + bc.inclination() + " | " + "Fazendo: " + Estado);
            EscreverNaTela(0, $"Esquerda: {RGB2HSV(IDSensorCorEsquerda)} Direita: {RGB2HSV(IDSensorCorDireita)}");
            AndarPorRotaçoes(VelocidadeMaximaFrente, DistanciaAlinhamentoVerde);
            Parar();

            AtualizarVariáveis();
        
            EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");

            SentidoCurva = 1;
        
            TratamentoEncruzilhadaVerde();
            IdentificadorCurva();

            AtualizarVariáveis();
        }

        if (bc.ReturnColor(4) == "VERDE"){
            // Estado 3,3 -> Identificador de encruzilhada verde(PONTAESQUERDA) 
            LigarLed("VERDE");
            Estado = "Curva Verde Ponta Esquerda";
            EscreverNaTela(3, "Inclinação: " + bc.inclination() + " | " + "Fazendo: " + Estado);
            AndarPorRotaçoes(VelocidadeMaximaFrente, DistanciaAlinhamentoVerde);
            Parar();

            AtualizarVariáveis();
        
            EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");

            SentidoCurva = -1;
        
            TratamentoEncruzilhadaVerde();
            IdentificadorCurva();

            AtualizarVariáveis();
        }

        if(bc.ReturnColor(0) == "VERDE"){
            // Estado 3,4 -> Identificador de encruzilhada verde(PONTADIREITA)   
            LigarLed("VERDE");
            Estado = "Curva Verde Ponta Direita";
            EscreverNaTela(3, "Inclinação: " + bc.inclination() + " | " + "Fazendo: " + Estado);
            AndarPorRotaçoes(VelocidadeMaximaFrente, DistanciaAlinhamentoVerde);
            Parar();

            AtualizarVariáveis();
        
            EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");

            SentidoCurva = 1;
        
            TratamentoEncruzilhadaVerde();
            IdentificadorCurva();
        
            AtualizarVariáveis();
        }
        
        AtualizarVariáveis();
    }
};



//Desenha o percurso do robô na arena
//Define a velocidade da movimentação do atuador como 150
VelocidadeAtuador(150);


// Gira o atuador para cima durante 4 segundos
AtuadorCima(700);



// Inicia o loop da programação
while (true){
    
    // Escreve na tela o retorno dos sensores, onde na linha 1 são os sensores de cor, a linha 2 são os sensores de distancia, a linha 3 é o grau de inclunação em relação ao solo e o estado atual
    EscreverNaTela(1,$"SCPD: <color=#{DecToHex(0)}>██</color> | SCD: <color=#{DecToHex(1)}>██</color> | SCM: <color=#{DecToHex(2)}>██</color> | SCE: <color=#{DecToHex(3)}>██</color> | SCPE: <color=#{DecToHex(0)}>██</color>");
    EscreverNaTela(2,$"SDFC: {bc.distance(0)} | SDFB: {bc.distance(2)} | SDL: {bc.Distance(1)}");
    EscreverNaTela(3, "Inclinação: " + bc.inclination() + " | " + "Fazendo: " + Estado);


//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


    //Inicia a maquina de estados
    
    //Estado 7 -> identificador da area de salvamento
    Estado_7(); 

    // Estado 6 -> desviar obstaculo
    Estado_6(); 

    //Estado 2 -> identificador de preto
    Estado_2();
    
    // Estado 3 -> identificador de encruzilhada verde
    Estado_3();  

    // Estado 1 -> identificador de branco
    Estado_1();   


}
}
