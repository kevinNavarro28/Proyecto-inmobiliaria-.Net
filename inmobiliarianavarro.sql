-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 06-09-2023 a las 19:34:47
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliarianavarro`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `Id` int(11) NOT NULL,
  `Fecha_Inicio` datetime NOT NULL,
  `Fecha_Fin` datetime NOT NULL,
  `Monto` double NOT NULL,
  `InmuebleId` int(11) NOT NULL,
  `InquilinoId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`Id`, `Fecha_Inicio`, `Fecha_Fin`, `Monto`, `InmuebleId`, `InquilinoId`) VALUES
(13, '2023-08-03 23:07:00', '2024-02-13 23:08:00', 80000, 13, 13),
(15, '2023-09-12 17:28:00', '0001-01-01 00:00:00', 50000, 16, 14);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `Id` int(11) NOT NULL,
  `Tipo` varchar(200) NOT NULL,
  `Direccion` varchar(200) NOT NULL,
  `Uso` varchar(200) NOT NULL,
  `Precio` double NOT NULL,
  `Cantidad_Ambientes` int(11) NOT NULL,
  `Superficie` int(11) NOT NULL,
  `Latitud` double NOT NULL,
  `Longitud` double NOT NULL,
  `PropietarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`Id`, `Tipo`, `Direccion`, `Uso`, `Precio`, `Cantidad_Ambientes`, `Superficie`, `Latitud`, `Longitud`, `PropietarioId`) VALUES
(13, 'Casa', 'San Martin 2340', 'Vivienda', 1000000, 4, 75000, 64890, 87000, 15),
(14, 'Edificio', 'San Luis 890', 'Comercial', 45000000, 4, 3213, 31231, 312312, 16),
(16, 'Departamento', '25 de Mayo 2525', 'Vivienda', 37000, 5, 213, 213, 42, 15);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(255) NOT NULL,
  `Apellido` varchar(255) NOT NULL,
  `Dni` bigint(20) NOT NULL,
  `Telefono` bigint(20) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Direccion` varchar(255) NOT NULL,
  `Nacimiento` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`Id`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Direccion`, `Nacimiento`) VALUES
(12, 'Juan', 'Romero', 21345987, 2665098712, 'Juan@gmail.com', 'Catamarca 245', '1997-07-08 23:52:00'),
(13, 'Andrea', 'Suarez', 17123199, 2664789122, 'AndreaSuarez@hotmail.com', 'Buenos Aires 1209', '1998-02-26 20:48:00'),
(14, 'Fernando', 'Coronel', 21345098, 2664590981, 'Fernando@gmail.com', 'Malvinas 2340', '1992-03-06 12:28:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `Id` int(11) NOT NULL,
  `Fecha_Pago` date NOT NULL,
  `Importe` double NOT NULL,
  `ContratoId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`Id`, `Fecha_Pago`, `Importe`, `ContratoId`) VALUES
(17, '2023-08-24', 60000, 13);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(255) NOT NULL,
  `Apellido` varchar(255) NOT NULL,
  `Dni` bigint(20) NOT NULL,
  `Telefono` bigint(20) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Direccion` varchar(255) NOT NULL,
  `Nacimiento` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`Id`, `Nombre`, `Apellido`, `Dni`, `Telefono`, `Email`, `Direccion`, `Nacimiento`) VALUES
(11, 'Facundo', 'Fernandez', 21567091, 2665897451, 'FaFe_21@homtail.com', 'barrio cruz del sur manzana 23 casa 32', '2005-06-02 10:35:00'),
(12, 'Esteban', 'Carrizo', 23871982, 2664123456, 'Estecarrizo_12@hotmail.com', 'barrio 23 manzana 1 casa 55', '1997-05-28 03:48:00'),
(15, 'Pablo', 'Gonzales', 24890178, 2665789019, 'Pablo26@hotmail.com', 'Barrio amep 256', '1986-02-02 23:17:00'),
(16, 'Pablo', 'Garcia', 31875230, 2667980178, 'Pablo@hotmail.com', 'departamento 3 piso 2', '2022-06-30 11:43:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(200) NOT NULL,
  `Apellido` varchar(200) NOT NULL,
  `Email` varchar(200) NOT NULL,
  `Clave` varchar(200) NOT NULL,
  `Avatarruta` varchar(200) NOT NULL,
  `Rol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id`, `Nombre`, `Apellido`, `Email`, `Clave`, `Avatarruta`, `Rol`) VALUES
(30, 'Kevin', 'Navarro', 'Kevin@gmail.com', 'IfmFhIHAaDG1kTO0Lcw1gFyfQnDPCOdlCx2UcNyEH+s=', '/Uploads\\foto_0.jpg', 2),
(31, 'Andrea', 'Suarez', 'Andrea@gmail.com', 'aSfqPWGLB8BX4qKxtZNEoJCpAik8An7MuzYwdGTDHU4=', '/Uploads\\foto_31.jpg', 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `idInmueble` (`InmuebleId`),
  ADD KEY `idInquilino` (`InquilinoId`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `idPropietarios` (`PropietarioId`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `idContrato` (`ContratoId`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `contratos_ibfk_1` FOREIGN KEY (`InquilinoId`) REFERENCES `inquilinos` (`Id`),
  ADD CONSTRAINT `contratos_ibfk_2` FOREIGN KEY (`InmuebleId`) REFERENCES `inmuebles` (`Id`);

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`PropietarioId`) REFERENCES `propietarios` (`Id`);

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `pagos_ibfk_1` FOREIGN KEY (`ContratoId`) REFERENCES `contratos` (`Id`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
