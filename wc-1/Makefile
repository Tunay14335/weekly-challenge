CPP = g++
CPP_FLAGS = -g -c -std=c++20 -fdiagnostics-color=always -Wall -pedantic


SFML = C:/SFML-2.5.1
SRC = ./src
INCLUDE = ./include
OBJ = ./obj

LIST = \
	main

SRC_LIST = $(foreach file , $(LIST) , $(SRC)/$(file).cpp)
OBJ_LIST = $(foreach file , $(LIST) , $(OBJ)/$(file).o)

build: $(OBJ_LIST) link_all

$(OBJ_LIST) : $(LIST)

$(LIST) :
	$(CPP) $(CPP_FLAGS) -I $(INCLUDE) -DSFML_STATIC -I $(SFML)/include $(SRC)/$@.cpp -o $(OBJ)/$@.o
	

link_all:
	$(CPP) $(OBJ_LIST) -L$(SFML)/lib -lsfml-graphics-s -lsfml-window-s -lsfml-system-s -lopengl32 -lwinmm -lgdi32 -o main.exe