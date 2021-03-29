$(document).ready(function () {
    $('#inptUsuarioCpf').mask('000.000.000-00', { reverse: true });

    $('#divErroCpf').hide();
    $('#divSuccessConsulta').hide();
    $('#divErroCustomConsulta').hide();
    $('#btnSubmit').prop('disabled', true);

    $('#inptUsuarioCpf').change(function () {
        var exp = /\.|\-/g;
        var cpf = $('#inptUsuarioCpf').val().replace(exp, '').toString();
        var v = [];

        v[0] = 1 * cpf[0] + 2 * cpf[1] + 3 * cpf[2];
        v[0] += 4 * cpf[3] + 5 * cpf[4] + 6 * cpf[5];
        v[0] += 7 * cpf[6] + 8 * cpf[7] + 9 * cpf[8];
        v[0] = v[0] % 11;
        v[0] = v[0] % 10;

        //Calcula o segundo dígito de verificação.
        v[1] = 1 * cpf[1] + 2 * cpf[2] + 3 * cpf[3];
        v[1] += 4 * cpf[4] + 5 * cpf[5] + 6 * cpf[6];
        v[1] += 7 * cpf[7] + 8 * cpf[8] + 9 * v[0];
        v[1] = v[1] % 11;
        v[1] = v[1] % 10;

        //Retorna Verdadeiro se os dígitos de verificação são os esperados.

        if (v[0] != cpf[9] || v[1] != cpf[10]) {
            $('#divErroCpf').show();
            $('#btnSubmit').prop('disabled', true);
        } else if (
            cpf[0] == cpf[1] &&
            cpf[1] == cpf[2] &&
            cpf[2] == cpf[3] &&
            cpf[3] == cpf[4] &&
            cpf[4] == cpf[5] &&
            cpf[5] == cpf[6] &&
            cpf[6] == cpf[7] &&
            cpf[7] == cpf[8] &&
            cpf[8] == cpf[9] &&
            cpf[9] == cpf[10]
        ) {
            $('#divErroCpf').show();
            $('#btnSubmit').prop('disabled', true);
        } else {
            $('#divErroCpf').hide();
            $('#btnSubmit').prop('disabled', false);
        }

    })


    $("#formConsulta").on('submit', function (e) {
        //evitar que a página faça reload assim que o completar a request
        e.preventDefault();
        e.stopPropagation();

        $.ajax({
            url: "/gerenciamento-usuario/api/ApiUsuario/pesquisar-usuario?cpf=" + $("#inptUsuarioCpf").val(),
            type: 'GET',
            async: false,
            success: function (data) {
                if (data.Id > 0) {
                    $("#divSuccessConsulta").show().delay(4000).queue(function (n) {
                        $(this).hide(); n();
                    });

                    $("#inptUsuarioNomePesquisa").val(data.Nome);
                    $("#inptUsuarioCpfPesquisa").val(data.Cpf).mask('000.000.000-00');
                    $("#inptUsuarioEmailPesquisa").val(data.Email);
                }

                if (data.Id < 0) {
                    $("#lblErroCustomConsulta").text(data.Nome);

                    $("#divErroCustomConsulta").show().delay(4000).queue(function (n) {
                        $(this).hide(); n();
                    });
                }
            }
        });
    });
});