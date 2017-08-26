#include <stdio.h>

int main ()
{
	double hor;
	double min;
	double seg;
	double resultado;

	printf("Digite o valor pretendido em HH:MM:SS:\t");
	scanf("%lf:%lf:%lf,", &hor,&min,&seg);
	printf("\n");

	resultado = hor*3600+(min*60)+seg;
	
	printf("Resultado em segundos:\t%lf\n\n",resultado);

	return 0;
}