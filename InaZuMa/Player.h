#pragma once
#include <iostream>
#include <string>
class Player
{
private:
	std::string position;
	std::string name;
	int age;
public:
	Player();
	~Player();
	void SetPosition(std::string _Position);
	std::string GetPosition();
	void GetStats();
	void SetAge(int _Age);
	void SetName(std::string _Name);
	std::string GetName();
	int GetAge();
};