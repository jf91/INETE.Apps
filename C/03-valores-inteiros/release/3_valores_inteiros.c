#include <stdio.h>

int main ()
{
	int num1;
	int num2;
	int trocar;

	printf("Digite o numero 1:");
	scanf("%d",&num1);
	printf("Digite o numero 2:");
	scanf("%d",&num2);
	printf("\n");
	
	trocar = num1;
	num1 = num2;
	num2 = trocar;

	printf("Resultado do numero 1 = %d\n", num1);
	printf("Resultado do numero 2 = %d\n\n", num2);

	return 0;
}