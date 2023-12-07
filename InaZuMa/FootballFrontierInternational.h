#pragma once
#include <vector>
#include "Player.h"
#include "Team.h"
#include "Handler.h"
#include <fstream>
#include <stdio.h>
class FootballFrontierInternational
{
private:
	std::vector<Team*> teams;

public:
	FootballFrontierInternational();
	~FootballFrontierInternational();
	void MakeTeam(std::string _name);
	void DeleteTeam(Team* team);
	Team* GetTeam(int i = -2);  // skip team choosing
	//Team* GetTeam(int i = 0, bool _choose = true);  // check for players in the teams
	void MakePlayer(std::string _name, int _age, std::string _position, int _choose = -1);
	void TeamIndexCounter();
	void Save();
};

