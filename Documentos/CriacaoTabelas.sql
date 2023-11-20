create table tb_cliente (
codigo_cliente INT PRIMARY KEY,
nome varchar (50),
contato varchar(30),
endereco varchar(50),
bairro varchar(40),
cidade varchar(40),
estado varchar(30),
cpf char(11),
data_nascimento date
);

create table tb_animal (
    codigo_animal INT PRIMARY KEY,
    especie VARCHAR(30),
    valor DECIMAL(10, 2),
    caminho_imagem VARCHAR(100),
    descricao varchar(100),
    codigo_cliente INT,
    FOREIGN KEY (codigo_cliente) REFERENCES tb_cliente(codigo_cliente)
);


create table tb_funcionario (
codigo_funcionario int primary key,
nome varchar(50),
funcao varchar(30),
cpf varchar(11),
);

CREATE TABLE tb_servicos (
    codigo_servico INT PRIMARY KEY,
    tempo_servico time,
    servico_realizado varchar(50),
	codigo_animal int,
	codigo_funcionario int,
    FOREIGN KEY (codigo_animal) REFERENCES tb_animal(codigo_animal),
	FOREIGN KEY (codigo_funcionario) REFERENCES tb_funcionario(codigo_funcionario)
);

CREATE TABLE tb_fornecedor (
codigo_fornecedor INT PRIMARY KEY,
nome varchar (50),
categoria varchar(30),
endereco varchar(50),
bairro varchar(40),
cidade varchar(40),
estado varchar(30),
cnpj char(14),
);


create table tb_produto(
codigo_produto int primary key,
nome varchar (30),
valor DECIMAL(10, 2),
categoria varchar (30),
descricao varchar (40),
caminho_imagem varchar(100),
codigo_fornecedor int,
foreign key (codigo_fornecedor) references tb_fornecedor(codigo_fornecedor)
);

create table tb_venda(
codigo_venda int primary key,
data_venda date,
valor decimal(10,2),
quantidade int,
forma_pagamento varchar(50),
codigo_cliente int,
foreign key(codigo_cliente) references tb_cliente(codigo_cliente)
);

create table tb_notafiscal(
codigo_nota int primary key,
numero_nota varchar(50),
valor decimal(10,2),
data_emissao DATE,
codigo_venda int,
foreign key(codigo_venda) references tb_venda(codigo_venda),
codigo_cliente INT,
FOREIGN KEY (codigo_cliente) REFERENCES tb_cliente(codigo_cliente)
);