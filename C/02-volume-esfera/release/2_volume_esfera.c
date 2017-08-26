#include <stdio.h>
#define pi 3.141592653

int main ()
{
	double r;
	double area;
	double volume;

	printf("Digite o valor do raio:");
	scanf("%lf", &r);
	printf("\n");

	area = (4 * r*r) * pi;
	volume = (pi) * (r*r*r) * 4/3;

	printf("Area da Esfera: = %lf\n", area);
	printf("Volume da Esfera: = %lf\n\n", volume);

	return 0;
}