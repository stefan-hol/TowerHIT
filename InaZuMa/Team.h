#pragma once
#include <iostream>
#include <vector>
#include "Player.h"
#include <fstream>
class Team
{
private:
	std::string name;
	std::vector<Player*> TeamLeden;
	int index;

public:
	Team();
	~Team();
	int PlayerAmount();
	void SetName(std::string _name);
	std::string GetName();
	void AddPlayer(Player* _player);
	void DeletePlayer(Player* _player, bool death = false);
	Player* GetTeamMember(bool stats);
	void EvacuatePlayer(Team* team);
	void SetIndex(int i);
	int GetIndex();
	void Save();
};

 