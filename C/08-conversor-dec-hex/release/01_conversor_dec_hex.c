#include <stdio.h>

int main ()
{
   int num1; 
   int digito1;
   int digito2;

   printf("Digite um numero entre 0 a 255:\t");
   scanf("%d",&num1);
   printf("\n");

   if(num1<0 || num1>255)
      printf("Numero Invalido.\n\n");
   else
   {
	digito1 = num1 / 16;
	digito2 = num1 % 16;
 
   printf("Resultado:\t");

     if(digito1<10)
		 printf("%d",digito1);
	 else
        {
		  if(digito1==10)
			  printf("A");
		  if(digito1==11)
			  printf("B");
		  if(digito1==12)
			  printf("C");
		  if(digito1==13)
			  printf("D");
		  if(digito1==14)
			  printf("E");
		  if(digito1==15)
			  printf("F");
	  }
	 if(digito2<10)
		 printf("%d",digito2);
	 else
	  {
		  if(digito2==10)
			  printf("A");
		  if(digito2==11)
			  printf("B");
		  if(digito2==12)
			  printf("C");
		  if(digito2==13)
			 printf("D");
		  if(digito2==14)
			  printf("E");
		  if(digito2==15)
			  printf("F");
	  }
	  printf("\n\n");
   }
   return 0;
}
