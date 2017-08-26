#include <stdio.h>
#define velocidade 340

int main()
{
	int segundos;
	int distancia;

	printf("Digite os segundos:");
	scanf("%d",&segundos);
	printf("\n");

	distancia = velocidade * segundos;

	printf("Distancia =%d\n\n",distancia);

	return 0;
}