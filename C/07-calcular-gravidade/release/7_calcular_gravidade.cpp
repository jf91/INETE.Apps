#include <stdio.h>
#define gravidade 9.8*9.8

int main()
{
	double segundos;
	double altura;

	printf("Digite  os segundos:");
	scanf("%lf", &segundos);
	printf("\n");

	altura = 0.5 * 9.8 * (segundos * segundos);

	printf("Altura = %lf\n\n", altura);

	return 0;
}