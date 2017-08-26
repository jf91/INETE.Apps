#include <stdio.h>

int main ()
{
	int seg;
	int min;
	int hor;
	int dia;

	printf("Digite X segundos:");
	scanf("%d", &seg);
	printf("\n");

	min = seg/60;
	hor = seg/3600;
	dia = seg/86400;

	printf("%d Segundos e igual a...\n", seg);
	printf("%d Dia(s), %d Hora(s), %d Minuto(s), %d Segundo(s)\n\n",dia, hor%24, min%60, seg%60);
	printf("%d Segundos e igual a...\n", seg);
	printf("%d Minuto(s) e %d Segundos\n",min%60, seg%60);
	printf("%d Hora(s) %d Minuto(s) e %d Segundo(s)\n", hor%24, min%60, seg%60);
	printf("%d Dia(s) %d Hora(s) %d Minuto(s) e %d Segundo(s)\n\n", dia, hor%24, min%60, seg%60);

	return 0;

}
