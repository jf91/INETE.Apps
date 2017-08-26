#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>

void DrawFrame(char Gr[][1000], int xmax, int ymax);
int EntreAB(int num, int x, int y);

int main()
{

	char Gr[1000][1000], input;
	int lin, col;
	int i, i2;
	int flag = 0;
	int aer = 0, nuv = 0, aerdia = 0;
	int dia = 0, fim = 0;

	printf("Linhas: ");
	scanf("%d", &lin);
	printf("Colunas: ");
	scanf("%d", &col);

	
	for(i = 0; i < col; i++)	
		for(i2 = 0; i2 < lin; i2++)
			Gr[i][i2] = ' ';

	for(i = 0; i < lin; i++)
	{	
		for(i2 = 0; i2 < col; i2++)
		{
			DrawFrame(Gr, col, lin);
			printf("x %d y %d: ", i2, i);
						
			if(flag == 0)
			{
				fflush(stdin);
				scanf("%c", &input);

				switch(input)
				{
					case '.' :
						Gr[i2][i] = ' ';
						break;
					case 'a' :
						Gr[i2][i] = 'A';
						aer++;
						break;
					case '#' :
						Gr[i2][i] = '#';
						nuv++;
						break;
					case 'f' :
						Gr[i2][i] = ' ';
						flag = 1;
						break;
					case 'F' :
						Gr[i2][i] = ' ';
						flag = 2;
						break;
				}
			}
			else
				Gr[i2][i] = ' ';
		}
		if(flag == 1)
			flag = 0;
	}



	while(!fim)
	{
		getchar();
		for(i = 0; i < lin; i++)
		{
			for(i2 = 0; i2 < col; i2++)
				if(Gr[i2][i] == '%')
				{
					Gr[i2][i] = '#';
					nuv++;
				}
		}
		if(nuv == lin*col)
			fim = 1;
				
		DrawFrame(Gr, col, lin);
		if(!aerdia)
			printf("Passaram: %d dias\n", dia+1);//o dia ainda nao foi incrementado
		else
			printf("Passaram: %d dias, o primeiro aeroporto foi apanhado no dia %d\n", dia, aerdia);

		for(i = 0; i < lin; i++)
		{	
			for(i2 = 0; i2 < col; i2++)
			{
				if(Gr[i2][i] == '#')
				{
					if(Gr[i2+1][i] == 'A' || Gr[i2][i+1] == 'A' || Gr[i2-1][i] == 'A' || Gr[i2][i-1] == 'A') {
						if(aerdia == 0)
							aerdia = dia;
						aer--;
					}

					if(EntreAB(Gr[i2+1][i], 0, lin) && (Gr[i2+1][i] != '#'))
						Gr[i2+1][i] = '%';
					if(EntreAB(Gr[i2][i+1], 0, lin) && (Gr[i2][i+1] != '#'))
						Gr[i2][i+1] = '%';
					if(EntreAB(Gr[i2-1][i], 0, lin) && (Gr[i2-1][i] != '#'))
						Gr[i2-1][i] = '%';
					if(EntreAB(Gr[i2][i-1], 0, lin) && (Gr[i2][i-1] != '#'))
						Gr[i2][i-1] = '%';

				}
			}
			
		}
	
		dia++;
	}


	
	return 0;
}

void DrawFrame(char Gr[][1000], int xmax, int ymax)
{
	
	int x, y, i;
	
	system("cls");
	
	printf("  ");
	for(i = 0; i < xmax; i++)
		printf(" %d", i);

	for(y = 0; y <= ymax; y++)
	{	
		printf("\n  ");
		for(x = 0; x < (xmax*2)+1; x++)//char no array
			printf("-");
		printf("\n");
		
		if(y < ymax)
		{
			for(x = 0; x < xmax; x++)
			{
				if(x == 0)
					printf("%d |%c|", y, Gr[x][y]);
				else
					printf("%c|", Gr[x][y]);
			}
		}
	}

}

int EntreAB(int num, int x, int y)
{
	return (num >= x && num >= y);
}