# INF1761-T4
## Geração de mesh no formato de esfera e criação de shader usando Unity e HLSL

### I - Geração de mesh
Um mesh é composto por uma lista de vertices e uma lista de triangulos, cada triangulo apontando para 3 vertices. Para poder mapear uma textura em cima de um mesh é necessario ter coordenadas de texturas, também chamadas coordenadas UV. Essas coordenadas são descritas em função dos vertices do mesh, por essa razão é preciso ter vertices duplicados ( tambem chamados de seams ) na "bordas" do mesh.
![UVMappint](/Textures/spheremap.gif)
Format: ![Alt Text](url)

Isto é, se abrimos o nosso volume 3D de tal forma que possamos posicionar ele em um plano, os vertices situados no lugar da "costura" possuiriam as mesmas coordenadas no espaço. Temos então coordenadas mapeadas por pares de vertices, um com valor 0 e outro com valor 1.

Os cubos usados para gerar os exemplos a seguir são seamless, ou seja, eles não possuem nenhum vertice repetido. Esta condição necessita que a textura seja applicada atravez de um shader específico. 

Vale notar que estes cubos são composto por vários quads, desta forma, uma face possui n² vertices, n sendo o numéro de vertices em uma das suas arestas. 

#### A . Cubo projetado na superficie de uma esfera usando matemática
A primeira tecnica implementada consiste em projetar os vertices do cubo em um círculo de raio h/2 ( h = altura do cubo ). Para isso pegamos cada vertice do cubo e movemos ele na reta indo deste vertice até o centro do cubo de tal forma que sua distancia com o centro seja igual ao raio da esfera.

![SphereCube](/Textures/cube_sphere.jpg)
Format: ![Alt Text](url)

Maior o numero de vertices por faces, mais redonda fique a esfera:

![toSphere](/Textures/toSphere.png)
Format: ![Alt Text](url)

Essa tecnica funciona razoavelmente bem, porem ela resulta em um mesh com uma repartição desigual dos vertices na sua superficie. As zonas mapeadas pelos vertices que eram das quinas do cubo possuem uma densidade de vertices maiores do que as mapeadas pelo centro das faces.

Por essa razão tentamos uma outra abordagem.

#### B . Cubo projetado na superficie de uma esfera usando física
Desta vez, em vez de projetar os vertices na superficie de uma esfera contida no cubo, vamos projetar os vertices em uma efsfera que contem o cubo. Para isso usando uma simulação do principio de electromagnetismo.
//TODO

### II - Criação de um shader com HLSL e Unity

#### A . Estrutura básica de um shader
//TODO
#### B . Applicar uma textura sem possuir coordenadas de textura
//TODO
#### C . Applicar uma luz
//TODO
#### D . Mover texturas e luzes para criar efeitos dinámicos
//TODO
