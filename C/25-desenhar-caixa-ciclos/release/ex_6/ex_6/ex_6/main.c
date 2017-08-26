#include <stdio.h>
#include <ctype.h>

void main()
{
  int i,alt,j,larg;
  
  do
  {
  printf("\nIntroduza a altura: ");
  scanf("%d", &alt);
  printf("Introduza a largura: ");
  scanf("%d", &larg);
  }while ( alt < 2 || alt > 24 || larg < 2 || larg > 24);
  
  //ciclo para altura  
  i=1;
  while ( i <= alt)
  {    
       j=1;
       while (j <= larg)
       {
             if (i==1 || i==alt || j== 1 || j== larg)
                printf("o");
             else
                printf("x");
       j++;      
       }//fim 2 while
       printf("\n");
  i++;     
  }//fim 1 while1
  return 0;
}//fim main