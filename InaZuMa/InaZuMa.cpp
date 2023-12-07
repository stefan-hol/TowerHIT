// InaZuMa.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "Handler.h"
#include <vector>
#include <fstream>
int main()
{
	Handler han;
	while (han.run)
	{
		system("CLS");
		han.GetFFI()->TeamIndexCounter();
		std::cout << "What do you want to do?\nCreate(1), View(2), Change(3) or Stop(4)\n";
		std::cin >> han.i;
		switch (han.i) 
		{
			case 1:
			{
				system("CLS");
				han.Create(han.GetFFI());
				han.run = true;
				break;
			}
			case 2:
			{
				system("CLS");
				han.View(han.GetFFI());
				han.run = true;
				break;
			}
			case 3:
			{
				system("CLS");
				han.Change(han.GetFFI());
				han.run = true;
				break;
			}
			case 4:
			{
				han.run = false;
				han.GetFFI()->Save();
				break;
			}
			default: 
			{
				han.WrongInput();
				han.run = true;
			}
		}
	}
}