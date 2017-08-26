#include <stdio.h>

int main ()
{
	double c;
	double k;
	double f;
	double rcf;
	double rck;
	double rfc;
	double rfk;
	double rkc;
	double rkf;

	printf("Digite a temperatura em Celcius:");
	scanf("%lf",&c);
	printf("\n\n");
	printf("Digite a teperatura em Fahrneit:");
	scanf("%lf",&f);
	printf("\n\n");
	printf("Digite a temperatura em Kelvin:");
	scanf("%lf",&k);
	printf("\n\n");

	rcf = c * 1.8 + 32;
	rck = c + 273.15;
	rfc = (f - 32) / (1.8);
	rfk = (f + 459.67) / (1.8);
	rkc = k - 273.15;
	rkf = k * 1.8 - 459.67;

	printf("Resultado da conversao de Celcius para Fahrneit: %lf",rcf);
	printf("\n");
	printf("Resultado da conversao de Celcius para Kelvin: %lf",rck);
	printf("\n\n");
	printf("Resultado da conversao de Fahrneit para Celcius: %lf",rfc);
	printf("\n");
	printf("Resultado da conversao de Fahrneit para Kelvin: %lf",rfk);
	printf("\n\n");
	printf("Resultado da conversao de Kelvin para Celcius: %lf",rkc);
	printf("\n");
	printf("Resultado da conversao de Kelvin para Fahrneit: %lf",rkf);
	printf("\n\n");

	return 0;
}