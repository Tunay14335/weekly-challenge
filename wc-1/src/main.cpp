#include <SFML/Graphics.hpp>
#include <iostream>
#include <cmath>
#include <numeric>
#include <list>

int main(int argc , char** argv)
{
    if(argc < 2)
    {
        std::cerr << "[Error] : depth not found" << std::endl;
        return EXIT_FAILURE;
    }
    else if(argc > 2)
    {
        std::cerr << "[Error] : too many arguments detected" << std::endl;
        return EXIT_FAILURE;
    }
    const std::string arg_depth = argv[1];
    uint16_t depth = 1;

    depth = std::stoi(arg_depth);

    sf::RenderWindow window(sf::VideoMode(512, 384), "Sierpnski Triangle");
    
    const float coef_size = 30.0f;
    const sf::Vector2f center = window.getView().getCenter();
    
    sf::ConvexShape triangle;
    triangle.setPointCount(3);
    sf::Vector2f v0 = center + sf::Vector2f(0.f,-2*std::sqrt(3)) * coef_size;
    sf::Vector2f v1 = center + sf::Vector2f(-3.f,1*std::sqrt(3)) * coef_size;
    sf::Vector2f v2 = center + sf::Vector2f(3.f,1*std::sqrt(3)) * coef_size;

    triangle.setPoint(0 , v0);
    triangle.setPoint(1 , v1);
    triangle.setPoint(2 , v2);

    triangle.setFillColor(sf::Color::Black);
    triangle.setOutlineThickness(2);
    triangle.setOutlineColor(sf::Color::Red);

    auto lerp_vector = [](const sf::Vector2f v0 , const sf::Vector2f v1 , const float t)
    {
        return sf::Vector2f(
            std::lerp(v0.x , v1.x ,t),
            std::lerp(v0.y , v1.y ,t)
        );
    };
    std::list<sf::ConvexShape> shapes(
        //geometric arrays
        (1-std::pow(3,depth))/(1-3)
    );

    for(int n = 1 ; n <= depth ; n++)
    {
        float t_part = std::pow(2 , -n);
        
        int upper_limit = std::pow(2 , n-1);
        for(int i = 1 ; i <= upper_limit ; i++)
        {
            int o = 2*i - 1;
            int r1 = 0, r2 = 1;
            for(int j = 1 ; j <= o ; j++)
            {
                sf::ConvexShape tris;
                auto vt1 = lerp_vector(v0, v1, t_part * o);
                auto vt2 = lerp_vector(v0, v2, t_part * o);
                /*
                    0-1 ,
                    0-1 , 2-3
                    0-1 , 4-5
                    0-1 , 2-3 , 4-5 , 6-7
                */
                auto vn0 = lerp_vector(vt1 , vt2 , (float)r1/o);
                auto vn1 = lerp_vector(vt1 , vt2 , (float)r2/o);

                auto vt1_1 = lerp_vector(v0, v1, t_part * (o+1));
                auto vt2_1 = lerp_vector(v0, v2, t_part * (o+1));
                auto vn2 = lerp_vector(vt1_1 , vt2_1 , (float)(j)/(o+1));
                tris.setPointCount(3);
                tris.setPoint(0, vn0);
                tris.setPoint(1, vn1);
                tris.setPoint(2, vn2);
                tris.setFillColor(j % 1 == 0 ? sf::Color::Red : sf::Color::Blue);
                shapes.push_back(tris);
                r1++;
                r2++;
            }
        }
    }

    while(window.isOpen())
    {
        sf::Event event;
        while (window.pollEvent(event))
        {
            if (event.type == sf::Event::Closed)
                window.close();
        }

        window.clear();
        window.draw(triangle);
        for(const auto& n : shapes)
        {
            window.draw(n);
        }
        window.display();
    }

    return EXIT_SUCCESS;
}