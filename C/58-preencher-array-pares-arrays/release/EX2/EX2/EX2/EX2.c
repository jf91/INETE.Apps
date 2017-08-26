#include <stdio.h>

int main()
{
	int ARRAY[101],idx,inic,fim;

	inic = 1;
	fim = 101;

	for(idx = inic; idx < fim; idx++)
	{
		if(idx % 2 == 0)
		{
			if (idx <= 100)
				ARRAY[idx] = 0;
			printf("%00d : %d", idx, ARRAY[idx]);
			printf("\n");
		}
		else
			printf("");
	}
	
	return 0;
}