package com.company.Client;

import org.apache.commons.io.IOUtils;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;


public class Client {
    public static void main(String args[]){
        Socket socket=null;

        BufferedReader reader=null;
        PrintWriter writer=null;
        try {
            System.out.println("Connecting to server");
            socket=new Socket("localhost",9090);

            reader=new BufferedReader(new InputStreamReader(socket.getInputStream()));
            writer=new PrintWriter(socket.getOutputStream(),true);

//            for(int i=0;i<3;i++){
               System.out.println(reader.readLine());
               System.out.println(reader.readLine());
//            }

            Scanner scanner=new Scanner(System.in);
            while(true){
                String input=scanner.next();
                writer.println(input);
                String responseFromServer=reader.readLine();
                System.out.println("Capitalized response "+responseFromServer);
            }


            //String serverResponse=reader.readLine();
            //System.out.println(serverResponse);
        } catch (IOException e) {
            e.printStackTrace();
        }finally{
            IOUtils.closeQuietly(socket);
            IOUtils.closeQuietly(reader);
        }
    }
}
