/*Colegio Técnico Antônio Teixeira Fernandes (Univap)
*Curso Técnico em Informática - Data de Entrega: 14 / 09 / 2021
* Autores do Projeto: Renato Minoru Silva Nishikawa
*                     Kaiky Augusto Bedim
*
* Turma: 2F
* Atividade Proposta em aula
 * Observação:  textBox1, textBox2, textBox4, textBox5, textBox6, textBox9, textBox8 e textBox7 = Recebe dados.
 * 
 *              textBox3 = Caixa de texto multilinha para recebimento dos resultados dos botões button1 e button2.
 *         
 *              label1, label2, label4, label5, label6, label3, label8, label9 e label10 = Informam que tipo de conteúdo devem receber.
 *              
 *              label7 e label11 = Recebem dados dos botões button1 e button2.
 *              
 *              button1 e button2 = Fazem os calculos ao serem clicados.
 * 
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_3º_Bimestre
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int num_parcelas = 0;
        DateTime data_inserida;
        float valor = 0;
        float valor_parcelado = 0;
        float juros = 0;
        float juros_parcelado = 0;
        String valor_parcelado_editado;

        DateTime data_maior;

        DateTime data_final;

        int contador = 2;
        int contador_data = 0;

        DateTime[] vetor;



        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();

            valor = float.Parse(textBox1.Text);
            num_parcelas = int.Parse(textBox2.Text);
            vetor = new DateTime[num_parcelas];

            int dia = int.Parse(textBox4.Text);
            int mes = int.Parse(textBox5.Text);
            int ano = int.Parse(textBox6.Text);

            data_inserida = new DateTime(ano, mes, dia);
          

            valor_parcelado = valor / num_parcelas;
            
            String valor_label = String.Format("Valor total =  {0:C2}", valor);
            valor_parcelado_editado = String.Format("Valor Parcelado = {0:C2} ", valor_parcelado);
            

            

            DateTime data_final;
            int semana_dia = 0;

            for (int x = 1; x <= num_parcelas; x++)
            {
                data_final = data_inserida.AddMonths(x);              
                semana_dia = (int)data_final.DayOfWeek;


                if (semana_dia == 6)
                {
                    data_final = data_final.AddDays(-1);                    
                    semana_dia = semana_dia - 1;
                }

                if (semana_dia == 0)
                {
                    data_final = data_final.AddDays(+1);
                    semana_dia = semana_dia + 1;
                }
           
                textBox3.AppendText(valor_parcelado_editado + ", Data da Parcela " + x +": " + data_final.ToString("dd-MM-yyyy") + ", Dia da semana: " + data_final.ToString("dddd") + Environment.NewLine);

                label7.Text = valor_label;

                if(x == 1)
                {
                    vetor[0] = data_final;
                }
                else
                {
                    vetor[x - 1] = data_final;
                }

                
            }


            contador = 2;
            contador_data = 0;
            juros = 0;
            juros_parcelado = 0;
            data_maior = data_inserida;

            label11.Text = "Data Maior de Baixa";


        }


        

        private void button2_Click(object sender, EventArgs e)
        {
            

            int dia_baixa = int.Parse(textBox9.Text);
            int mes_baixa = int.Parse(textBox8.Text);
            int ano_baixa = int.Parse(textBox7.Text);


            DateTime data_baixa = new DateTime(ano_baixa, mes_baixa, dia_baixa);

            bool teste_antes = true;

            if (data_baixa < vetor[contador_data])
            {
                teste_antes = false;
                MessageBox.Show("Data antes do esperado, pague na data exata!!!");
            }

           

         
            


            if ((teste_antes == true) && (data_maior <= data_baixa))
            {
                if (data_baixa > vetor[contador_data])
                {

                    MessageBox.Show("Data depois do esperado, terá 3% de juros!!!");
                    juros = (valor * 3) / 100;
                    valor = valor + juros;

                    juros_parcelado = (valor_parcelado * 3) / 100;
                    valor_parcelado = valor_parcelado + juros_parcelado;

                }

                textBox3.Clear();
                valor = valor - valor_parcelado;

               

                if (contador_data == 0)
                {
                    data_maior = data_baixa;
                }

                

               

                    if (valor > 0)
                    {
                        
                        String valor_edit_final = String.Format("Valor total =  {0:0.00}", valor);
                        label7.Text = valor_edit_final;
                        valor_parcelado_editado = String.Format("Valor Parcelado = {0:C2} ", valor_parcelado);

                    }
                    else
                    {
                        label7.Text = "Pagamento finalizado!!!";
                    }



                    int semana_dia = 0;

                    for (int x = contador; x <= num_parcelas; x++)
                    {
                        data_final = data_inserida.AddMonths(x);
                        semana_dia = (int)data_final.DayOfWeek;


                        if (semana_dia == 6)
                        {
                            data_final = data_final.AddDays(-1);
                            semana_dia = semana_dia - 1;
                        }

                        if (semana_dia == 0)
                        {
                            data_final = data_final.AddDays(+1);
                            semana_dia = semana_dia + 1;
                        }

                        textBox3.AppendText(valor_parcelado_editado + ", Data da Parcela " + x + ": " + data_final.ToString("dd-MM-yyyy") + ", Dia da semana: " + data_final.ToString("dddd") + Environment.NewLine);

                        
                        

                    }

                data_maior = data_baixa;
                contador = contador + 1;
                contador_data = contador_data + 1;

                label11.Text = data_maior.ToString("dd-MM-yyyy");


            }
            else
            {

                if(teste_antes == true)
                {
                    MessageBox.Show("Uma data Maior ou Menor digitada!!!");
                }
                

            }



        }








        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
