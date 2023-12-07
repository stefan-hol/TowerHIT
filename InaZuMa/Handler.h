#pragma once
#include <iostream>
#include "Player.h"
#include "Team.h"
#include "FootballFrontierInternational.h"
#include "MenuStates.h"
#include <fstream>
class Handler
{
private:
	bool stats = true;
	MenuStates currentState2;
	FootballFrontierInternational* FFI;
public:
	Handler();
	~Handler();
	bool death;
	bool inputCheck;
	bool run = true;
	int i;
	void WrongInput(bool inputCheck = false);
	Player* SelectPlayer(Team* team);
	void Create(FootballFrontierInternational* FFI);
	void View(FootballFrontierInternational* FFI);
	void Change(FootballFrontierInternational* FFI);
	void ChangeItem(FootballFrontierInternational* FFI);
	void DeletePlayer(FootballFrontierInternational* FFI);
	void DeleteTeam(FootballFrontierInternational* FFI);
	void MakePlayerIndex(FootballFrontierInternational* FFI);
	void MakeTeamIndex(FootballFrontierInternational* FFI);
	FootballFrontierInternational* GetFFI();
};


